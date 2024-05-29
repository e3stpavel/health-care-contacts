export type Style = 'default' | 'ghost'

// eslint-disable-next-line antfu/top-level-function
export const labelStyles = (style: Style) => ({
  'text-gray-900 font-medium': style === 'default',
  'text-gray-500': style === 'ghost',
})
