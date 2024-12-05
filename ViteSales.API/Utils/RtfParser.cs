using System.Globalization;
using System.Text.RegularExpressions;

namespace ViteSales.API.Utils;

public class RtfParser
{

    private static readonly Regex RtfRegex = new (@"\\([a-z]{1,32})(-?\d{1,10})?[ ]?|\\'([0-9a-f]{2})|\\([^a-z])|([{}])|[\r\n]+|(.)",
        RegexOptions.Singleline | RegexOptions.IgnoreCase);

    private static readonly List<string> Destinations = new ()
    {
        "aftncn", "aftnsep", "aftnsepc", "annotation", "atnauthor", "atndate", "atnicn", "atnid",
        "atnparent", "atnref", "atntime", "atrfend", "atrfstart", "author", "background",
        "bkmkend", "bkmkstart", "blipuid", "buptim", "category", "colorschememapping",
        "colortbl", "comment", "company", "creatim", "datafield", "datastore", "defchp", "defpap",
        "do", "doccomm", "docvar", "dptxbxtext", "ebcend", "ebcstart", "factoidname", "falt",
        "fchars", "ffdeftext", "ffentrymcr", "ffexitmcr", "ffformat", "ffhelptext", "ffl",
        "ffname", "ffstattext", "field", "file", "filetbl", "fldinst", "fldrslt", "fldtype",
        "fname", "fontemb", "fontfile", "fonttbl", "footer", "footerf", "footerl", "footerr",
        "footnote", "formfield", "ftncn", "ftnsep", "ftnsepc", "g", "generator", "gridtbl",
        "header", "headerf", "headerl", "headerr", "hl", "hlfr", "hlinkbase", "hlloc", "hlsrc",
        "hsv", "htmltag", "info", "keycode", "keywords", "latentstyles", "lchars", "levelnumbers",
        "leveltext", "lfolevel", "linkval", "list", "listlevel", "listname", "listoverride",
        "listoverridetable", "listpicture", "liststylename", "listtable", "listtext",
        "lsdlockedexcept", "macc", "maccPr", "mailmerge", "maln", "malnScr", "manager", "margPr",
        "mbar", "mbarPr", "mbaseJc", "mbegChr", "mborderBox", "mborderBoxPr", "mbox", "mboxPr",
        "mchr", "mcount", "mctrlPr", "md", "mdeg", "mdegHide", "mden", "mdiff", "mdPr", "me",
        "mendChr", "meqArr", "meqArrPr", "mf", "mfName", "mfPr", "mfunc", "mfuncPr", "mgroupChr",
        "mgroupChrPr", "mgrow", "mhideBot", "mhideLeft", "mhideRight", "mhideTop", "mhtmltag",
        "mlim", "mlimloc", "mlimlow", "mlimlowPr", "mlimupp", "mlimuppPr", "mm", "mmaddfieldname",
        "mmath", "mmathPict", "mmathPr", "mmaxdist", "mmc", "mmcJc", "mmconnectstr",
        "mmconnectstrdata", "mmcPr", "mmcs", "mmdatasource", "mmheadersource", "mmmailsubject",
        "mmodso", "mmodsofilter", "mmodsofldmpdata", "mmodsomappedname", "mmodsoname",
        "mmodsorecipdata", "mmodsosort", "mmodsosrc", "mmodsotable", "mmodsoudl",
        "mmodsoudldata", "mmodsouniquetag", "mmPr", "mmquery", "mmr", "mnary", "mnaryPr",
        "mnoBreak", "mnum", "mobjDist", "moMath", "moMathPara", "moMathParaPr", "mopEmu",
        "mphant", "mphantPr", "mplcHide", "mpos", "mr", "mrad", "mradPr", "mrPr", "msepChr",
        "mshow", "mshp", "msPre", "msPrePr", "msSub", "msSubPr", "msSubSup", "msSubSupPr", "msSup",
        "msSupPr", "mstrikeBLTR", "mstrikeH", "mstrikeTLBR", "mstrikeV", "msub", "msubHide",
        "msup", "msupHide", "mtransp", "mtype", "mvertJc", "mvfmf", "mvfml", "mvtof", "mvtol",
        "mzeroAsc", "mzeroDesc", "mzeroWid", "nesttableprops", "nextfile", "nonesttables",
        "objalias", "objclass", "objdata", "object", "objname", "objsect", "objtime", "oldcprops",
        "oldpprops", "oldsprops", "oldtprops", "oleclsid", "operator", "panose", "password",
        "passwordhash", "pgp", "pgptbl", "picprop", "pict", "pn", "pnseclvl", "pntext", "pntxta",
        "pntxtb", "printim", "private", "propname", "protend", "protstart", "protusertbl", "pxe",
        "result", "revtbl", "revtim", "rsidtbl", "rxe", "shp", "shpgrp", "shpinst",
        "shppict", "shprslt", "shptxt", "sn", "sp", "staticval", "stylesheet", "subject", "sv",
        "svb", "tc", "template", "themedata", "title", "txe", "ud", "upr", "userprops",
        "wgrffmtfilter", "windowcaption", "writereservation", "writereservhash", "xe", "xform",
        "xmlattrname", "xmlattrvalue", "xmlclose", "xmlname", "xmlnstbl",
        "xmlopen"
    };

    private static readonly Dictionary<string, string> SpecialCharacters = new ()
    {
        { "par", "\n" },
        { "sect", "\n\n" },
        { "page", "\n\n" },
        { "line", "\n" },
        { "tab", "\t" },
        { "emdash", "\u2014" },
        { "endash", "\u2013" },
        { "emspace", "\u2003" },
        { "enspace", "\u2002" },
        { "qmspace", "\u2005" },
        { "bullet", "\u2022" },
        { "lquote", "\u2018" },
        { "rquote", "\u2019" },
        { "ldblquote", "\u201C" },
        { "rdblquote", "\u201D" }
    };

    public static string StripRichTextFormat(string inputRtf)
    {
        if (inputRtf == null)
        {
            return null;
        }

        string returnString;

        var stack = new Stack<StackEntry>();
        var ignorable = false; // Whether this group (and all inside it) are "ignorable".
        var ucskip = 1; // Number of ASCII characters to skip after a unicode character.
        var curskip = 0; // Number of ASCII characters left to skip
        var outList = new List<string>(); // Output buffer.

        var matches = RtfRegex.Matches(inputRtf);

        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                var word = match.Groups[1].Value;
                var arg = match.Groups[2].Value;
                var hex = match.Groups[3].Value;
                var character = match.Groups[4].Value;
                var brace = match.Groups[5].Value;
                var tchar = match.Groups[6].Value;

                if (!string.IsNullOrEmpty(brace))
                {
                    curskip = 0;
                    if (brace == "{")
                    {
                        // Push state
                        stack.Push(new (ucskip, ignorable));
                    }
                    else if (brace == "}")
                    {
                        // Pop state
                        var entry = stack.Pop();
                        ucskip = entry.NumberOfCharactersToSkip;
                        ignorable = entry.Ignorable;
                    }
                }
                else if (!string.IsNullOrEmpty(character)) // \x (not a letter)
                {
                    curskip = 0;
                    if (character == "~")
                    {
                        if (!ignorable)
                        {
                            outList.Add("\xA0");
                        }
                    }
                    else if ("{}\\".Contains(character))
                    {
                        if (!ignorable)
                        {
                            outList.Add(character);
                        }
                    }
                    else if (character == "*")
                    {
                        ignorable = true;
                    }
                }
                else if (!string.IsNullOrEmpty(word)) // \foo
                {
                    curskip = 0;
                    if (Destinations.Contains(word))
                    {
                        ignorable = true;
                    }
                    else if (ignorable)
                    {
                    }
                    else if (SpecialCharacters.ContainsKey(word))
                    {
                        outList.Add(SpecialCharacters[word]);
                    }
                    else if (word == "uc")
                    {
                        ucskip = int.Parse(arg);
                    }
                    else if (word == "u")
                    {
                        var c = int.Parse(arg);
                        if (c < 0)
                        {
                            c += 0x10000;
                        }

                        //Ein gültiger UTF32-Wert ist zwischen 0x000000 und 0x10ffff (einschließlich) und sollte keine Ersatzcodepunktwerte (0x00d800 ~ 0x00dfff)
                        if (c >= 0x000000 && c <= 0x10ffff && (c < 0x00d800 || c > 0x00dfff))
                            outList.Add(char.ConvertFromUtf32(c));
                        else outList.Add("?");
                        curskip = ucskip;
                    }
                }
                else if (!string.IsNullOrEmpty(hex)) // \'xx
                {
                    if (curskip > 0)
                    {
                        curskip -= 1;
                    }
                    else if (!ignorable)
                    {
                        var c = int.Parse(hex, NumberStyles.HexNumber);
                        outList.Add(char.ConvertFromUtf32(c));
                    }
                }
                else if (!string.IsNullOrEmpty(tchar))
                {
                    if (curskip > 0)
                    {
                        curskip -= 1;
                    }
                    else if (!ignorable)
                    {
                        outList.Add(tchar);
                    }
                }
            }
        }
        else
        {
            returnString = inputRtf;
        }

        returnString = string.Join(string.Empty, outList.ToArray());

        return returnString;
    }

    private class StackEntry(int numberOfCharactersToSkip, bool ignorable)
    {
        public int NumberOfCharactersToSkip { get; } = numberOfCharactersToSkip;
        public bool Ignorable { get; } = ignorable;
    }
}