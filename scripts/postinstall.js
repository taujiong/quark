const { execSync } = require('child_process')

const isCI = require('ci-info').isCI
if (!isCI) {
  require('husky').install()
  execSync('dotnet tool restore')
}
