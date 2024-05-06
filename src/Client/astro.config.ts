import { defineConfig } from 'astro/config'

import unocss from 'unocss/astro'
import node from '@astrojs/node'

// https://astro.build/config
export default defineConfig({
  output: 'server',

  adapter: node({
    mode: 'standalone',
  }),

  integrations: [
    unocss({
      injectReset: '@unocss/reset/tailwind-compat.css',
    }),
  ],
})
