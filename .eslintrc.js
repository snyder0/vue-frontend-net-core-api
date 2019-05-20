module.exports = {
  root: true,
  env: {
    node: true,
  },
  extends: [
    //'plugin:vue/recommended',
    '@vue/typescript',
  ],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'vue/html-indent': [ 'warn', 'tab'],
  },
  parserOptions: {
    parser: '@typescript-eslint/parser',
  },
  parser: 'vue-eslint-parser',
  plugins: ['@typescript-eslint'],
};
