const isCI = require('ci-info').isCI
if (!isCI) {
  require('husky').install()
}
