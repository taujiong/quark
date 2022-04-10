/* eslint-disable */
module.exports = {
  name: '@yarnpkg/plugin-dotenv',
  factory: function (require) {
    var plugin
    plugin = (() => {
      var e = {
          86: (e, r, n) => {
            'use strict'
            n.r(r), n.d(r, { hooks: () => i })
            var t = n(347),
              o = n(843),
              s = n.n(o)
            const i = {
              async setupScriptEnvironment(e, r) {
                var n
                const o = (0, t.v)({
                  path: null !== (n = s()('.env')) && void 0 !== n ? n : void 0,
                })
                Object.assign(r, o.parsed)
              },
            }
          },
          347: (e, r, n) => {
            const t = n(747),
              o = n(622)
            function s(e) {
              console.log('[dotenv][DEBUG] ' + e)
            }
            const i = /^\s*([\w.-]+)\s*=\s*(.*)?\s*$/,
              u = /\\n/g,
              c = /\n|\r|\r\n/
            function l(e, r) {
              const n = Boolean(r && r.debug),
                t = {}
              return (
                e
                  .toString()
                  .split(c)
                  .forEach(function (e, r) {
                    const o = e.match(i)
                    if (null != o) {
                      const e = o[1]
                      let r = o[2] || ''
                      const n = r.length - 1,
                        s = '"' === r[0] && '"' === r[n]
                      ;("'" === r[0] && "'" === r[n]) || s
                        ? ((r = r.substring(1, n)),
                          s && (r = r.replace(u, '\n')))
                        : (r = r.trim()),
                        (t[e] = r)
                    } else n && s(`did not match key and value when parsing line ${r + 1}: ${e}`)
                  }),
                t
              )
            }
            e.exports.v = function (e) {
              let r = o.resolve(process.cwd(), '.env'),
                n = 'utf8',
                i = !1
              e &&
                (null != e.path && (r = e.path),
                null != e.encoding && (n = e.encoding),
                null != e.debug && (i = !0))
              try {
                const e = l(t.readFileSync(r, { encoding: n }), { debug: i })
                return (
                  Object.keys(e).forEach(function (r) {
                    Object.prototype.hasOwnProperty.call(process.env, r)
                      ? i &&
                        s(
                          `"${r}" is already defined in \`process.env\` and will not be overwritten`
                        )
                      : (process.env[r] = e[r])
                  }),
                  { parsed: e }
                )
              } catch (e) {
                return { error: e }
              }
            }
          },
          843: (e, r, n) => {
            'use strict'
            var t = n(747),
              o = n(622),
              s = n(588),
              i = t.readFileSync,
              u = t.statSync,
              c = o.join,
              l = o.resolve,
              a = /^\./,
              p = o.sep
            function d(e, r, n) {
              r = c(e, r)
              var t = c(r, n)
              return u(t) && { cwd: e, dir: r, path: t }
            }
            function f(e, r, t) {
              r = c(e, r)
              var o = c(r, t),
                s = n(603).resolve(o)
              return s && { cwd: e, dir: r, path: s }
            }
            function v(e, r) {
              var n = g(e, r)
              return n && n.path
            }
            function g(e, r) {
              if (!e) return null
              var n,
                t =
                  null !== (r = r || {}).dir && void 0 !== r.dir
                    ? r.dir
                    : '.config',
                o = r.dot ? e : e.replace(a, ''),
                i = r.module ? f : d,
                u = l(r.cwd || '.').split(p),
                c = u.length
              function v(r) {
                try {
                  return i(r, '', e)
                } catch (e) {}
                try {
                  return i(r, t, o)
                } catch (e) {}
              }
              for (; c--; ) {
                if ((n = v(u.join(p)))) return n
                u.pop()
              }
              return (r.home || null === r.home || void 0 === r.home) &&
                (n = v(s))
                ? n
                : null
            }
            ;(e.exports = v),
              (e.exports.obj = g),
              (e.exports.read = function (e, r) {
                if (!e) return null
                var n = v(e, (r = r || {}))
                return (
                  n && i(n, { encoding: r.encoding || 'utf8', flag: r.flag })
                )
              }),
              (e.exports.require = function (e, r) {
                if (!e) return null
                ;(r = r || {}).module = !0
                var t = v(e, r)
                return t && n(603)(t)
              })
          },
          603: (e) => {
            function r(e) {
              var r = new Error("Cannot find module '" + e + "'")
              throw ((r.code = 'MODULE_NOT_FOUND'), r)
            }
            ;(r.keys = () => []), (r.resolve = r), (r.id = 603), (e.exports = r)
          },
          918: (e, r, n) => {
            'use strict'
            var t = n(87)
            e.exports =
              'function' == typeof t.homedir
                ? t.homedir
                : function () {
                    var e = process.env,
                      r = e.HOME,
                      n = e.LOGNAME || e.USER || e.LNAME || e.USERNAME
                    return 'win32' === process.platform
                      ? e.USERPROFILE || e.HOMEDRIVE + e.HOMEPATH || r || null
                      : 'darwin' === process.platform
                      ? r || (n ? '/Users/' + n : null)
                      : 'linux' === process.platform
                      ? r ||
                        (0 === process.getuid()
                          ? '/root'
                          : n
                          ? '/home/' + n
                          : null)
                      : r || null
                  }
          },
          588: (e, r, n) => {
            'use strict'
            e.exports = n(918)()
          },
          747: (e) => {
            'use strict'
            e.exports = require('fs')
          },
          87: (e) => {
            'use strict'
            e.exports = require('os')
          },
          622: (e) => {
            'use strict'
            e.exports = require('path')
          },
        },
        r = {}
      function n(t) {
        if (r[t]) return r[t].exports
        var o = (r[t] = { exports: {} })
        return e[t](o, o.exports, n), o.exports
      }
      return (
        (n.n = (e) => {
          var r = e && e.__esModule ? () => e.default : () => e
          return n.d(r, { a: r }), r
        }),
        (n.d = (e, r) => {
          for (var t in r)
            n.o(r, t) &&
              !n.o(e, t) &&
              Object.defineProperty(e, t, { enumerable: !0, get: r[t] })
        }),
        (n.o = (e, r) => Object.prototype.hasOwnProperty.call(e, r)),
        (n.r = (e) => {
          'undefined' != typeof Symbol &&
            Symbol.toStringTag &&
            Object.defineProperty(e, Symbol.toStringTag, { value: 'Module' }),
            Object.defineProperty(e, '__esModule', { value: !0 })
        }),
        n(86)
      )
    })()
    return plugin
  },
}
