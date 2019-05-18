
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using PICOCompiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                            =  0, // (EOF)
        SYMBOL_ERROR                          =  1, // (Error)
        SYMBOL_WHITESPACE                     =  2, // Whitespace
        SYMBOL_MINUS                          =  3, // '-'
        SYMBOL_LPAREN                         =  4, // '('
        SYMBOL_RPAREN                         =  5, // ')'
        SYMBOL_COMMA                          =  6, // ','
        SYMBOL_COLON                          =  7, // ':'
        SYMBOL_COLONEQ                        =  8, // ':='
        SYMBOL_SEMI                           =  9, // ';'
        SYMBOL__DIGITS                        = 10, // '_Digits'
        SYMBOL__ID                            = 11, // '_Id'
        SYMBOL__LETTER                        = 12, // '_Letter'
        SYMBOL__QUOTE                         = 13, // '_Quote'
        SYMBOL__SPACE                         = 14, // '_Space'
        SYMBOL_PIPEPIPE                       = 15, // '||'
        SYMBOL_PLUS                           = 16, // '+'
        SYMBOL_BEGIN                          = 17, // begin
        SYMBOL_DECLARE                        = 18, // declare
        SYMBOL_DO                             = 19, // do
        SYMBOL_ELSE                           = 20, // else
        SYMBOL_END                            = 21, // end
        SYMBOL_FI                             = 22, // fi
        SYMBOL_FOR                            = 23, // for
        SYMBOL_IF                             = 24, // if
        SYMBOL_NATURAL                        = 25, // natural
        SYMBOL_OD                             = 26, // od
        SYMBOL_STRING                         = 27, // string
        SYMBOL_THEN                           = 28, // then
        SYMBOL_WHILE                          = 29, // while
        SYMBOL_ANYMINUSCHARMINUSBUTMINUSQUOTE = 30, // <any-char-but-quote>
        SYMBOL_ASSIGN                         = 31, // <assign>
        SYMBOL_CONC                           = 32, // <conc>
        SYMBOL_DECLS                          = 33, // <decls>
        SYMBOL_DIGITS                         = 34, // <digits>
        SYMBOL_EMPTY                          = 35, // <empty>
        SYMBOL_EXP                            = 36, // <exp>
        SYMBOL_FOR2                           = 37, // <for>
        SYMBOL_FORMINUSHEAD                   = 38, // <for-head>
        SYMBOL_ID                             = 39, // <id>
        SYMBOL_IDMINUSTYPEMINUSLIST           = 40, // <id-type-list>
        SYMBOL_IF2                            = 41, // <if>
        SYMBOL_IFMINUSCOMPACT                 = 42, // <if-compact>
        SYMBOL_LAYOUT                         = 43, // <layout>
        SYMBOL_LETTER                         = 44, // <letter>
        SYMBOL_LITERAL                        = 45, // <literal>
        SYMBOL_MINUS2                         = 46, // <minus>
        SYMBOL_NATURALMINUSCONSTANT           = 47, // <natural-constant>
        SYMBOL_PICOMINUSPROGRAM               = 48, // <pico-program>
        SYMBOL_PLUS2                          = 49, // <plus>
        SYMBOL_PRIMARY                        = 50, // <primary>
        SYMBOL_QUOTE                          = 51, // <quote>
        SYMBOL_SERIES                         = 52, // <series>
        SYMBOL_SPACE                          = 53, // <space>
        SYMBOL_STAT                           = 54, // <stat>
        SYMBOL_STRINGMINUSCONSTANT            = 55, // <string-constant>
        SYMBOL_STRINGMINUSTAIL                = 56, // <string-tail>
        SYMBOL_TYPE                           = 57, // <type>
        SYMBOL_WHILE2                         = 58  // <while>
    };

    enum RuleConstants : int
    {
        RULE_ID__ID                 =  0, // <id> ::= '_Id'
        RULE_LETTER__LETTER         =  1, // <letter> ::= '_Letter'
        RULE_DIGITS__DIGITS         =  2, // <digits> ::= '_Digits'
        RULE_SPACE__SPACE           =  3, // <space> ::= '_Space'
        RULE_QUOTE__QUOTE           =  4, // <quote> ::= '_Quote'
        RULE_EMPTY                  =  5, // <empty> ::= 
        RULE_LITERAL_LPAREN         =  6, // <literal> ::= '('
        RULE_LITERAL_RPAREN         =  7, // <literal> ::= ')'
        RULE_LITERAL_PLUS           =  8, // <literal> ::= '+'
        RULE_LITERAL_MINUS          =  9, // <literal> ::= '-'
        RULE_LITERAL_SEMI           = 10, // <literal> ::= ';'
        RULE_LITERAL_PIPEPIPE       = 11, // <literal> ::= '||'
        RULE_LITERAL_COLON          = 12, // <literal> ::= ':'
        RULE_LITERAL_COLONEQ        = 13, // <literal> ::= ':='
        RULE_NATURALCONSTANT        = 14, // <natural-constant> ::= <digits>
        RULE_LAYOUT                 = 15, // <layout> ::= <space>
        RULE_ANYCHARBUTQUOTE        = 16, // <any-char-but-quote> ::= <letter>
        RULE_ANYCHARBUTQUOTE2       = 17, // <any-char-but-quote> ::= <digits>
        RULE_ANYCHARBUTQUOTE3       = 18, // <any-char-but-quote> ::= <literal>
        RULE_ANYCHARBUTQUOTE4       = 19, // <any-char-but-quote> ::= <layout>
        RULE_STRINGTAIL             = 20, // <string-tail> ::= <any-char-but-quote> <string-tail>
        RULE_STRINGTAIL2            = 21, // <string-tail> ::= <quote>
        RULE_STRINGCONSTANT         = 22, // <string-constant> ::= <quote> <string-tail>
        RULE_IDTYPELIST_COLON       = 23, // <id-type-list> ::= <id> ':' <type> <empty>
        RULE_IDTYPELIST_COLON_COMMA = 24, // <id-type-list> ::= <id> ':' <type> ',' <id-type-list>
        RULE_DECLS_DECLARE_SEMI     = 25, // <decls> ::= declare <id-type-list> ';'
        RULE_TYPE_NATURAL           = 26, // <type> ::= natural
        RULE_TYPE_STRING            = 27, // <type> ::= string
        RULE_SERIES                 = 28, // <series> ::= <empty>
        RULE_SERIES2                = 29, // <series> ::= <stat> <empty>
        RULE_SERIES_SEMI            = 30, // <series> ::= <stat> ';' <series>
        RULE_STAT                   = 31, // <stat> ::= <assign>
        RULE_STAT2                  = 32, // <stat> ::= <if>
        RULE_STAT3                  = 33, // <stat> ::= <while>
        RULE_STAT4                  = 34, // <stat> ::= <for>
        RULE_ASSIGN_COLONEQ         = 35, // <assign> ::= <id> ':=' <exp>
        RULE_EXP                    = 36, // <exp> ::= <primary>
        RULE_EXP2                   = 37, // <exp> ::= <plus>
        RULE_EXP3                   = 38, // <exp> ::= <minus>
        RULE_EXP4                   = 39, // <exp> ::= <conc>
        RULE_PRIMARY                = 40, // <primary> ::= <id>
        RULE_PRIMARY2               = 41, // <primary> ::= <natural-constant>
        RULE_PRIMARY3               = 42, // <primary> ::= <string-constant>
        RULE_PRIMARY_LPAREN_RPAREN  = 43, // <primary> ::= '(' <exp> ')'
        RULE_PLUS_PLUS              = 44, // <plus> ::= <exp> '+' <primary>
        RULE_MINUS_MINUS            = 45, // <minus> ::= <exp> '-' <primary>
        RULE_CONC_PIPEPIPE          = 46, // <conc> ::= <exp> '||' <primary>
        RULE_IF_FI                  = 47, // <if> ::= <if-compact> fi
        RULE_IF_ELSE_FI             = 48, // <if> ::= <if-compact> else <series> fi
        RULE_IFCOMPACT_IF_THEN      = 49, // <if-compact> ::= if <exp> then <series>
        RULE_WHILE_WHILE_DO_OD      = 50, // <while> ::= while <exp> do <series> od
        RULE_FORHEAD_SEMI_SEMI      = 51, // <for-head> ::= <assign> ';' <exp> ';' <assign>
        RULE_FOR_FOR_DO_OD          = 52, // <for> ::= for <for-head> do <series> od
        RULE_PICOPROGRAM_BEGIN_END  = 53  // <pico-program> ::= begin <decls> <series> end
    };

    public class PICOParser
    {
        private LALRParser parser;

        /*** MODIFICAÇÕES ***/
        private MainForm _mainForm;
        private LinkedList<Dictionary<string,string>> _errorList = new LinkedList<Dictionary<string,string>>();
        public PICOParser(MainForm mainForm, String filename) : this( filename )
        {
            _mainForm = mainForm;
        }
        /*** MODIFICAÇÕES ***/

        public PICOParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public PICOParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public PICOParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
            parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            parser.Parse(source);

        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                args.Continue = false;            
                //todo: Report message to UI?
            }
        }

        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLONEQ :
                //':='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL__DIGITS :
                //'_Digits'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL__ID :
                //'_Id'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL__LETTER :
                //'_Letter'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL__QUOTE :
                //'_Quote'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL__SPACE :
                //'_Space'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PIPEPIPE :
                //'||'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BEGIN :
                //begin
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARE :
                //declare
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //end
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FI :
                //fi
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NATURAL :
                //natural
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OD :
                //od
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THEN :
                //then
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ANYMINUSCHARMINUSBUTMINUSQUOTE :
                //<any-char-but-quote>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONC :
                //<conc>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLS :
                //<decls>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGITS :
                //<digits>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EMPTY :
                //<empty>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR2 :
                //<for>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORMINUSHEAD :
                //<for-head>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDMINUSTYPEMINUSLIST :
                //<id-type-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF2 :
                //<if>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFMINUSCOMPACT :
                //<if-compact>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LAYOUT :
                //<layout>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LETTER :
                //<letter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LITERAL :
                //<literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS2 :
                //<minus>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NATURALMINUSCONSTANT :
                //<natural-constant>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PICOMINUSPROGRAM :
                //<pico-program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS2 :
                //<plus>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARY :
                //<primary>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_QUOTE :
                //<quote>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SERIES :
                //<series>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SPACE :
                //<space>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STAT :
                //<stat>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGMINUSCONSTANT :
                //<string-constant>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGMINUSTAIL :
                //<string-tail>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE2 :
                //<while>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                args.Continue = false;            
                //todo: Report message to UI?
            }
        }

        public static Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_ID__ID :
                //<id> ::= '_Id'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LETTER__LETTER :
                //<letter> ::= '_Letter'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DIGITS__DIGITS :
                //<digits> ::= '_Digits'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SPACE__SPACE :
                //<space> ::= '_Space'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_QUOTE__QUOTE :
                //<quote> ::= '_Quote'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EMPTY :
                //<empty> ::= 
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_LPAREN :
                //<literal> ::= '('
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_RPAREN :
                //<literal> ::= ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_PLUS :
                //<literal> ::= '+'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_MINUS :
                //<literal> ::= '-'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_SEMI :
                //<literal> ::= ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_PIPEPIPE :
                //<literal> ::= '||'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_COLON :
                //<literal> ::= ':'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LITERAL_COLONEQ :
                //<literal> ::= ':='
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NATURALCONSTANT :
                //<natural-constant> ::= <digits>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_LAYOUT :
                //<layout> ::= <space>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ANYCHARBUTQUOTE :
                //<any-char-but-quote> ::= <letter>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ANYCHARBUTQUOTE2 :
                //<any-char-but-quote> ::= <digits>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ANYCHARBUTQUOTE3 :
                //<any-char-but-quote> ::= <literal>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ANYCHARBUTQUOTE4 :
                //<any-char-but-quote> ::= <layout>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STRINGTAIL :
                //<string-tail> ::= <any-char-but-quote> <string-tail>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STRINGTAIL2 :
                //<string-tail> ::= <quote>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STRINGCONSTANT :
                //<string-constant> ::= <quote> <string-tail>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IDTYPELIST_COLON :
                //<id-type-list> ::= <id> ':' <type> <empty>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IDTYPELIST_COLON_COMMA :
                //<id-type-list> ::= <id> ':' <type> ',' <id-type-list>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_DECLS_DECLARE_SEMI :
                //<decls> ::= declare <id-type-list> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPE_NATURAL :
                //<type> ::= natural
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPE_STRING :
                //<type> ::= string
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SERIES :
                //<series> ::= <empty>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SERIES2 :
                //<series> ::= <stat> <empty>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SERIES_SEMI :
                //<series> ::= <stat> ';' <series>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STAT :
                //<stat> ::= <assign>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STAT2 :
                //<stat> ::= <if>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STAT3 :
                //<stat> ::= <while>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STAT4 :
                //<stat> ::= <for>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_COLONEQ :
                //<assign> ::= <id> ':=' <exp>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <primary>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <plus>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXP3 :
                //<exp> ::= <minus>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EXP4 :
                //<exp> ::= <conc>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARY :
                //<primary> ::= <id>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARY2 :
                //<primary> ::= <natural-constant>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARY3 :
                //<primary> ::= <string-constant>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PRIMARY_LPAREN_RPAREN :
                //<primary> ::= '(' <exp> ')'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PLUS_PLUS :
                //<plus> ::= <exp> '+' <primary>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_MINUS_MINUS :
                //<minus> ::= <exp> '-' <primary>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_CONC_PIPEPIPE :
                //<conc> ::= <exp> '||' <primary>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IF_FI :
                //<if> ::= <if-compact> fi
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IF_ELSE_FI :
                //<if> ::= <if-compact> else <series> fi
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_IFCOMPACT_IF_THEN :
                //<if-compact> ::= if <exp> then <series>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_WHILE_WHILE_DO_OD :
                //<while> ::= while <exp> do <series> od
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FORHEAD_SEMI_SEMI :
                //<for-head> ::= <assign> ';' <exp> ';' <assign>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FOR_FOR_DO_OD :
                //<for> ::= for <for-head> do <series> od
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PICOPROGRAM_BEGIN_END :
                //<pico-program> ::= begin <decls> <series> end
                //todo: Create a new object using the stored user objects.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            /*** MODIFICAÇÕES ***/
            MessageBox.Show("The file has been read.");
            /*** MODIFICAÇÕES ***/
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            /*** MODIFICAÇÕES ***/
            String file_name = _mainForm.getFileName();
            String error_type = "Lexical error";
            String line_number = args.Token.Location.LineNr.ToString();
            String col_number = args.Token.Location.ColumnNr.ToString();
            String error_description = args.Token.ToString();
            _insertError(file_name, error_type, line_number, col_number, error_description);
            /*** MODIFICAÇÕES ***/
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            /*** MODIFICAÇÕES ***/
            String file_name = _mainForm.getFileName();
            String error_type = "Syntax error";
            String line_number = args.UnexpectedToken.Location.LineNr.ToString();
            String col_number = args.UnexpectedToken.Location.ColumnNr.ToString();
            String error_description = "Error at .." + args.UnexpectedToken.ToString();

            String unexpectedToken = args.UnexpectedToken.ToString();
            String expectedTokens = args.ExpectedTokens.ToString();        
            String[] aExpectedTokens = expectedTokens.Split(' ');        
            String nextToken = aExpectedTokens[0];

            int token_id = 0; 
            foreach (SymbolConstants symbol in (Enum.GetValues(typeof(SymbolConstants))))
            {
                String symbol_token = symbol.ToString()
                    .Replace("SYMBOL_", "")
                    .ToLower();

                if(symbol_token == nextToken)
                {
                    token_id = (int) symbol;
                    break;
                }
            }
            
            if (token_id == 0)
            {
                //
                // não percebi o que ele tentou fazer aqui - WIP
                //

                /*for (int vv = 0; vv < 3; vv++)
                {
                    String k = Dibujo.Mi_Clase.e2.ElementAt(vv);
                    if (k == tokensiguiente_nombre)
                    {
                        idtoken = Dibujo.Mi_Clase.e1.ElementAt(vv);
                        break;
                    }
                }*/
            }

            _insertError(file_name, error_type, line_number, col_number, error_description);

            Location location = new Location(0, 0, 0);
            args.NextToken = new TerminalToken(new SymbolTerminal(token_id, nextToken), "", location);
            args.Continue = ContinueMode.Insert;
            /*** MODIFICAÇÕES ***/
        }

        private void _insertError(String file_name, String error_type, String line_number, String col_number, String error_description)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("file_name", file_name);
            d.Add("error_type", error_type);
            d.Add("line_number", line_number);
            d.Add("col_number", col_number);
            d.Add("error_description", error_description);
            _errorList.AddLast(d);
        }


    }
}
