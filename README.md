# Utterly Complete Health Care
Web service for Utterly Complete facilities and contact mechanisms

![Logotype](https://github.com/users/e3stpavel/projects/6/assets/70956582/d8c777d1-7350-424a-b167-aca5675b0ff9)

Before you dive into the development, take a minute to read [Contribution Guidelines](/.github/CONTRIBUTING.md).

## API Development
### Project structure
We have the following projects inside the solution:
  * `Domain` - entities, domain specific stuff
  * `Infrastructure.Data` - database connection, `DbContext`, repositories live here
  * `ApplicationCore` - DTOs, mappers and services live here
  * `WebAPI` - controllers and setup (as it is a startup project)

### Migrations
When using migrations make sure you set __Default Project__ to __`Infrastructure.Data`__. Otherwise you will not be able to create or apply migrations.

To apply migrations use Package Manager Console:
```
Update-Database
```

To create migration (when changes to `Domain` were made) use:
```
Add-Migration <name>
```

## Client Development
### Prerequisites
  * `node` `v20.12.2`
  * `pnpm`

### Project structure
Refer to the [Astro docs](https://docs.astro.build/en/basics/project-structure/).

### Dependencies
Use `pnpm` to install dependencies
```
pnpm install
```

### Run dev server
While developing use:
```
pnpm dev
```

### Build
To build use:
```
pnpm build
```
