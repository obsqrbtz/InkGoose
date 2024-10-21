import js from '@eslint/js'
import pluginVue from 'eslint-plugin-vue'

export default [
    js.configs.recommended,
    ...pluginVue.configs['flat/recommended'],
    {
        rules: {
            'vue/require-default-prop': 'off',
            'vue/html-indent': ['error', 4],
            'vue/singleline-html-element-content-newline': 0,
            'vue/component-name-in-template-casing': ['error', 'PascalCase'],
            'vue/multi-word-component-names': 'off'
        }
    }
]