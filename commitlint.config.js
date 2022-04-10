module.exports = {
  extends: ['@commitlint/config-conventional'],
  rules: {
    'header-max-length': [0],
    'scope-max-length': [2, 'always', 10],
    'subject-max-length': [2, 'always', 100],
    'body-max-length': [2, 'always', 250],
  },
}
