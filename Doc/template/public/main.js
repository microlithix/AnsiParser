import { ebnf } from './ebnf.js'

export default {
    configureHljs: (hljs) => {
        hljs.registerLanguage('ebnf', ebnf);
    },
}
