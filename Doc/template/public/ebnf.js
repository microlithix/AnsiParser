export function ebnf(hljs) {

    var nonterminal = {
        scope: 'variable',
        match: /<[a-zA-Z][a-zA-Z0-9-]*>/
    };

    var coded_terminal = {
        // Numeric terminal representing character code.
        scope: 'number',
        match: /[0-9][a-zA-z0-9]*/
    };

    var substitution = {
        scope: 'operator',
        begin: /::=/
    }

    return {
        aliases: ['ebnf'],
        contains: [
            substitution,
            nonterminal,
            coded_terminal,
            hljs.APOS_STRING_MODE,  // Literal terminal using single quotes.
            hljs.QUOTE_STRING_MODE, // Literal terminal using double quotes.
            hljs.C_LINE_COMMENT_MODE
        ]
    };
}
