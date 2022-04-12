import chalk from 'chalk'

export const logger = {
  info: (msg) => console.log(`${chalk.cyan('Info')}: ${msg}`),
  warn: (msg) => console.log(`${chalk.yellow('Warn')}: ${msg}`),
  error: (msg) => {
    if (msg instanceof Error) {
      console.log(`${chalk.red('Error')}: ${msg.message}`)
      msg.stack && console.log(msg.stack)
    } else {
      console.log(`${chalk.red('Error')}: ${msg}`)
    }
  },
}
