import { defineConfig } from 'astro/config'

import unocss from 'unocss/astro'

// https://astro.build/config
export default defineConfig({
  // TODO: add ssr

  integrations: [
    unocss({
      injectReset: '@unocss/reset/tailwind-compat.css',
    }),
  ],
})
