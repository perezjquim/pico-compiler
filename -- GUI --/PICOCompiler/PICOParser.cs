
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
        SYMBOL_EOF                  =  0, // (EOF)
        SYMBOL_ERROR                =  1, // (Error)
        SYMBOL_WHITESPACE           =  2, // Whitespace
        SYMBOL_MINUS                =  3, // '-'
        SYMBOL_LPAREN               =  4, // '('
        SYMBOL_RPAREN               =  5, // ')'
        SYMBOL_COMMA                =  6, // ','
        SYMBOL_COLON                =  7, // ':'
        SYMBOL_COLONEQ              =  8, // ':='
        SYMBOL_SEMI                 =  9, // ';'
        SYMBOL__DIGITS              = 10, // '_Digits'
        SYMBOL__ID                  = 11, // '_Id'
        SYMBOL__STRING              = 12, // '_String'
        SYMBOL_PIPEPIPE             = 13, // '||'
        SYMBOL_PLUS                 = 14, // '+'
        SYMBOL_BEGIN                = 15, // begin
        SYMBOL_DECLARE              = 16, // declare
        SYMBOL_DO                   = 17, // do
        SYMBOL_ELSE                 = 18, // else
        SYMBOL_END                  = 19, // end
        SYMBOL_FI                   = 20, // fi
        SYMBOL_FOR                  = 21, // for
        SYMBOL_IF                   = 22, // if
        SYMBOL_NATURAL              = 23, // natural
        SYMBOL_OD                   = 24, // od
        SYMBOL_STRING               = 25, // string
        SYMBOL_THEN                 = 26, // then
        SYMBOL_WHILE                = 27, // while
        SYMBOL_ASSIGN               = 28, // <assign>
        SYMBOL_CONC                 = 29, // <conc>
        SYMBOL_DECLS                = 30, // <decls>
        SYMBOL_EMPTY                = 31, // <empty>
        SYMBOL_EXP                  = 32, // <exp>
        SYMBOL_FOR2                 = 33, // <for>
        SYMBOL_FORMINUSHEAD         = 34, // <for-head>
        SYMBOL_ID                   = 35, // <id>
        SYMBOL_IDMINUSTYPEMINUSLIST = 36, // <id-type-list>
        SYMBOL_IF2                  = 37, // <if>
        SYMBOL_IFMINUSCOMPACT       = 38, // <if-compact>
        SYMBOL_MINUS2               = 39, // <minus>
        SYMBOL_NATURALMINUSCONSTANT = 40, // <natural-constant>
        SYMBOL_PICOMINUSPROGRAM     = 41, // <pico-program>
        SYMBOL_PLUS2                = 42, // <plus>
        SYMBOL_PRIMARY              = 43, // <primary>
        SYMBOL_SERIES               = 44, // <series>
        SYMBOL_STAT                 = 45, // <stat>
        SYMBOL_STRINGMINUSCONSTANT  = 46, // <string-constant>
        SYMBOL_TYPE                 = 47, // <type>
        SYMBOL_WHILE2               = 48  // <while>
    };

    enum RuleConstants : int
    {
        RULE_ID__ID                  =  0, // <id> ::= '_Id'
        RULE_STRINGCONSTANT__STRING  =  1, // <string-constant> ::= '_String'
        RULE_EMPTY                   =  2, // <empty> ::= 
        RULE_NATURALCONSTANT__DIGITS =  3, // <natural-constant> ::= '_Digits'
        RULE_IDTYPELIST_COLON        =  4, // <id-type-list> ::= <id> ':' <type> <empty>
        RULE_IDTYPELIST_COLON_COMMA  =  5, // <id-type-list> ::= <id> ':' <type> ',' <id-type-list>
        RULE_DECLS_DECLARE_SEMI      =  6, // <decls> ::= declare <id-type-list> ';'
        RULE_TYPE_NATURAL            =  7, // <type> ::= natural
        RULE_TYPE_STRING             =  8, // <type> ::= string
        RULE_SERIES                  =  9, // <series> ::= <empty>
        RULE_SERIES2                 = 10, // <series> ::= <stat> <empty>
        RULE_SERIES_SEMI             = 11, // <series> ::= <stat> ';' <series>
        RULE_STAT                    = 12, // <stat> ::= <assign>
        RULE_STAT2                   = 13, // <stat> ::= <if>
        RULE_STAT3                   = 14, // <stat> ::= <while>
        RULE_STAT4                   = 15, // <stat> ::= <for>
        RULE_ASSIGN_COLONEQ          = 16, // <assign> ::= <id> ':=' <exp>
        RULE_EXP                     = 17, // <exp> ::= <primary>
        RULE_EXP2                    = 18, // <exp> ::= <plus>
        RULE_EXP3                    = 19, // <exp> ::= <minus>
        RULE_EXP4                    = 20, // <exp> ::= <conc>
        RULE_PRIMARY                 = 21, // <primary> ::= <id>
        RULE_PRIMARY2                = 22, // <primary> ::= <natural-constant>
        RULE_PRIMARY3                = 23, // <primary> ::= <string-constant>
        RULE_PRIMARY_LPAREN_RPAREN   = 24, // <primary> ::= '(' <exp> ')'
        RULE_PLUS_PLUS               = 25, // <plus> ::= <exp> '+' <primary>
        RULE_MINUS_MINUS             = 26, // <minus> ::= <exp> '-' <primary>
        RULE_CONC_PIPEPIPE           = 27, // <conc> ::= <exp> '||' <primary>
        RULE_IF_FI                   = 28, // <if> ::= <if-compact> fi
        RULE_IF_ELSE_FI              = 29, // <if> ::= <if-compact> else <series> fi
        RULE_IFCOMPACT_IF_THEN       = 30, // <if-compact> ::= if <exp> then <series>
        RULE_WHILE_WHILE_DO_OD       = 31, // <while> ::= while <exp> do <series> od
        RULE_FORHEAD_SEMI_SEMI       = 32, // <for-head> ::= <assign> ';' <exp> ';' <assign>
        RULE_FOR_FOR_DO_OD           = 33, // <for> ::= for <for-head> do <series> od
        RULE_PICOPROGRAM_BEGIN_END   = 34  // <pico-program> ::= begin <decls> <series> end
    };

    public class PICOParser
    {
        private LALRParser parser;

        /*** MODIFICAÇÕES ***/
        private MainForm _mainForm;

        // Lista de erros (sendo que cada erro é composto por um array de strings - posição 0 -> error_type, posição 1 -> line_number, ...)
        private LinkedList<string[]> _errorList = new LinkedList<string[]>();

        private TreeNode _tree = new TreeNode();

        // Guarda os tipos de dados associados a cada variável declarada no ficheiro .pico processado
        private Dictionary<string, string> _declarations = new Dictionary<string,string>();

        public PICOParser(MainForm mainForm, String filename) : this( filename )
        {
            _mainForm = mainForm;
        }
        /*** MODIFICAÇÕES ***/
        
        /*************************************************************/
        public LinkedList<string[]> GetErrorList()
        {
            return _errorList;
        }

        public void SetErrorList(LinkedList<string[]> value)
        {
            _errorList = value;
        }
        public TreeNode GetTree()
        {
            return _tree;
        }
        public void SetTree(TreeNode tree)
        {
            _tree = tree;
        }
        /*************************************************************/

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
                MessageBox.Show(e.ToString());
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

                case (int)SymbolConstants.SYMBOL__STRING :
                //string
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

                case (int)SymbolConstants.SYMBOL_THEN :
                //then
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
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

                case (int)SymbolConstants.SYMBOL_SERIES :
                //<series>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STAT :
                //<stat>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING:
                //<string-constant>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGMINUSCONSTANT :
                //<string-constant>
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
                MessageBox.Show(e.ToString());
                args.Continue = false;            
                //todo: Report message to UI?
            }
        }

        public Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_ID__ID :
                //<id> ::= '_Id'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_STRINGCONSTANT__STRING :
                //<string-constant> ::= '_String'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_EMPTY :
                //<empty> ::= 
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_NATURALCONSTANT__DIGITS:
                //<natural-constant> ::= <digits>
                //todo: Create a new object using the stored user objects.
                return null;               

                case (int)RuleConstants.RULE_IDTYPELIST_COLON :
                    //<id-type-list> ::= <id> ':' <type> <empty>
                    //todo: Create a new object using the stored user objects.

               // return null;

                case (int)RuleConstants.RULE_IDTYPELIST_COLON_COMMA :
                    //<id-type-list> ::= <id> ':' <type> ',' <id-type-list>
                    //todo: Create a new object using the stored user objects.

                    string decl_var_name = ((TerminalToken)((NonterminalToken)token.Tokens[0]).Tokens[0]).Text;
                    string decl_var_type = ((TerminalToken)((NonterminalToken)token.Tokens[2]).Tokens[0]).Text;

                    _declarations.Add(decl_var_name, decl_var_type + "-constant");
                    return null;

                case (int)RuleConstants.RULE_DECLS_DECLARE_SEMI :
                //<decls> ::= declare <id-type-list> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TYPE_NATURAL :
                //<type> ::= natural
                //todo: Create a new object using the stored user objects.
                //_declarations.Add(token.Tokens[0].ToString(), "natural-constant");
                return null;

                case (int)RuleConstants.RULE_TYPE_STRING :
                //<type> ::= string
                //todo: Create a new object using the stored user objects.
                //_declarations.Add(token.Tokens[0].ToString(), "string-constant");
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
                    string actual_var_type = _getActualVarType(token);
                    if(actual_var_type == null)
                    {
                        String file_name = _mainForm.getFileName();
                        String error_type = "Syntax error";
                        Location error_location = ((TerminalToken)((NonterminalToken)token.Tokens[0]).Tokens[0]).Location;
                        String line_number = (error_location.LineNr + 1).ToString();
                        String col_number = error_location.ColumnNr.ToString();
                        String error_description = "Undeclared variable '"+ ((TerminalToken)((NonterminalToken)token.Tokens[0]).Tokens[0]).Text +"'.";
                        _insertError(file_name, error_type, line_number, col_number, error_description);
                    }

                    else if(!_isTypeValid(token,actual_var_type))
                    {
                        String file_name = _mainForm.getFileName();
                        String error_type = "Syntax error";
                        Location error_location = ((TerminalToken)((NonterminalToken)token.Tokens[0]).Tokens[0]).Location;
                        String line_number = (error_location.LineNr + 1).ToString();
                        String col_number = error_location.ColumnNr.ToString();
                        String error_description = "Invalid data type of assigned value to variable '" + ((TerminalToken)((NonterminalToken)token.Tokens[0]).Tokens[0]).Text + "'.";
                        _insertError(file_name, error_type, line_number, col_number, error_description);
                    }

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
                    _parseTree(token);
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            /*** MODIFICAÇÕES ***/
            MessageBox.Show("The file has been read succesfully");
            /*** MODIFICAÇÕES ***/
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            /*** MODIFICAÇÕES ***/
            String file_name = _mainForm.getFileName();
            String error_type = "Lexical error";
            String line_number = args.Token.Location.LineNr.ToString();
            String col_number = args.Token.Location.ColumnNr.ToString();
            String error_description = "Unrecognized symbol at .. " +args.Token.ToString();
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
            String error_description = "Error at .. " + args.UnexpectedToken.ToString();

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

            _insertError(file_name, error_type, line_number, col_number, error_description);

            Location location = new Location(0, 0, 0);
            args.NextToken = new TerminalToken(new SymbolTerminal(token_id, nextToken), "", location);
            args.Continue = ContinueMode.Insert;
            /*** MODIFICAÇÕES ***/
        }

        private void _insertError(String file_name, String error_type, String line_number, String col_number, String error_description)
        {
            String[] error = { file_name, error_type, line_number, col_number, error_description };
            _errorList.AddLast(error);
        }

        private string _getActualVarType(NonterminalToken token)
        {
            string assign_var_name = ((TerminalToken)((NonterminalToken)token.Tokens[0]).Tokens[0]).Text;
            string actual_var_type;
            _declarations.TryGetValue(assign_var_name, out actual_var_type);

            return actual_var_type;
        }

        private bool _isTypeValid(Token token, string actual_var_type)
        {        
            if (token.GetType() == typeof(TerminalToken))
            {
                switch (((TerminalToken)token).Symbol.Id)
                {
                    case (int)SymbolConstants.SYMBOL__DIGITS:
                        return actual_var_type.Equals("natural-constant");

                    case (int)SymbolConstants.SYMBOL__STRING:
                        return actual_var_type.Equals("string-constant");

                    case (int)SymbolConstants.SYMBOL__ID:
                        string assign_var_type;
                        _declarations.TryGetValue(((TerminalToken)token).Text.ToString(), out assign_var_type);
                        return actual_var_type.Equals(assign_var_type);
                }
                return false;
            }
            else
            {
                if (((NonterminalToken)token).Tokens.Length > 1)
                {
                    return _isTypeValid(((NonterminalToken)token).Tokens[0], actual_var_type) && _isTypeValid(((NonterminalToken)token).Tokens[2], actual_var_type);
                }
                else
                {
                    return _isTypeValid(((NonterminalToken)token).Tokens[0], actual_var_type);
                }
            }

        }
        
        private void _parseTree(Token token)
        {
            if (token is NonterminalToken)
            {
                TreeNode node = _tree.Nodes.Add(((NonterminalToken)token).Rule.ToString());
                foreach (Token t in ((NonterminalToken)token).Tokens)
                {
                    _parseTree(t, node);
                }
            }
            else
            {
                _tree.Nodes.Add(((TerminalToken)token).Text.ToString());
            }
        }
        
        private void _parseTree(Token token, TreeNode prevNode)
        {
            if (token is NonterminalToken)
            {
                TreeNode node = prevNode.Nodes.Add(((NonterminalToken)token).Rule.ToString());
                foreach (Token t in ((NonterminalToken)token).Tokens)
                {
                    _parseTree(t, node);
                }
            }
            else
            {
                prevNode.Nodes.Add(((TerminalToken)token).Text.ToString());
            }
        }            
    }
}
