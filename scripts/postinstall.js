import { isCI } from 'ci-info'
import { execaCommand } from 'execa'

if (!isCI) {
  import('husky').then((m) => m.install())
  execaCommand('dotnet tool restore')
}
