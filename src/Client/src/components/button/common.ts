export interface AsLinkProps {
  as: 'a'
  href: string
}

export interface AsButtonProps {
  as: 'button'
  type: HTMLButtonElement['type']
}

export type AsProps = AsLinkProps | AsButtonProps
