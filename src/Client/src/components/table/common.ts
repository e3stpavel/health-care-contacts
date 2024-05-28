export const sizes = ['sm', 'md', 'lg', 'xl', '2xl'] as const

export type Size = typeof sizes[number]

export type CellProps = {
  suppressOnMobile: true
  breakpoint: Size
} | { suppressOnMobile?: false }
