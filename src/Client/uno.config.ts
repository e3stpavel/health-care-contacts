import { defineConfig, transformerDirectives, transformerVariantGroup } from 'unocss'

import uno, { type Theme } from 'unocss/preset-uno'
import icons from 'unocss/preset-icons'

import { theme } from 'unocss/preset-mini'

const sans = theme.fontFamily.sans

export default defineConfig<Theme>({
  presets: [
    // https://unocss.dev/presets/uno
    uno(),

    // https://unocss.dev/presets/icons
    icons(),
  ],

  transformers: [
    // https://unocss.dev/transformers/directives
    transformerDirectives(),

    // https://unocss.dev/transformers/variant-group
    transformerVariantGroup(),
  ],

  theme: {
    colors: {
      brand: {
        'blue': '#03668D',
        'green': '#02523D',
        'navy-blue': '#023C52',
      },
    },
    fontFamily: {
      sans: `-apple-system,BlinkMacSystemFont,'Pretendard Std Variable','Pretendard Std',Pretendard,ui-sans-serif,system-ui,${sans.split(',').slice(4).join(',')}`,
    },
  },

  preflights: [
    {
      getCSS: () => `
        :root {
          color-scheme: light dark;
        }

        *:focus-visible {
          outline-width: medium;
          outline-offset: -1px;
        }
      `,
    },
    {
      getCSS: ({ theme: { fontFamily, maxWidth } }) => `
        html, :host {
          font-family: ${fontFamily?.sans};
        }

        h1, h2, h3, h4, h5, h6 {
          text-wrap: balance;
        }

        p, li {
          text-wrap: pretty;
          max-width: ${maxWidth?.prose ?? '65ch'};
        }
      `,
    },
  ],
})
