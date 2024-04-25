# Contribution guidelines
The following document outlines the guidelines on how to contribute to the project. As a side note, it is also worth reading the [following guide from GitHub](https://github.com/firstcontributions/first-contributions).

## Projects tab
You can track all the progress, add ideas and create issues using [GitHub Projects](). You can find the attached project under the projects tab.

![Project](image-1.png)

## Create branch
When you assigned a new issue, you should create a new branch (usually from `main`) and give it a meaningful name which somehow describes the scope (what are you doing in this branch). Try to keep branch names short and concise.

_Example_

__Issue.__ Fix Party <-> Contact Mechanism relation

__Possible branch names.__ `party-contacts`, `fix-party-contacts`, `party-contacts-relation`

## Clone the project
When you are cloning the project make sure that you installed all the dependencies and the project runs on your machine. In case of problems do not hesitate to add new issue. Usually Visual Studio does install all the dependencies itself automatically, whereas for `Client` project you need to use command `pnpm install`.

## Apply migrations
When you are working with the ASP.NET project (`WebAPI`) make sure to apply the migrations using `dotnet` cli or using Package Manager Console and command `update-database`. This will create the database (if does not exist) and apply all the migrations to match the expected schema. 

## Commit
While creating commits please stick to the [semantic commit messages](https://gist.github.com/joshbuchea/6f47e86d2510bce28f8e7f42ae84c716). 

> Format: `<type>(<scope>): <subject>`

### Scope
Branch name

### Types
Type | Description
---|---
`feat` | new feature for the user, not a new feature for build script
`fix` | bug fix for the user, not a fix to a build script
`docs` | changes to the documentation
`style` | formatting, missing semi colons, etc; no production code change
`refactor` | refactoring production code, eg. renaming a variable
`test` | adding missing tests, refactoring tests; no production code change
`chore` | updating grunt tasks etc; no production code change

### Subject
Meaningful, but small/medium length, commit message. Answers the question: what changes did you apply in this commit?

## Create Pull Request
When you are done with your changes push them to the remote repository. After that, you should create a Pull Request (PR).

### Name
Please keep PR names clear and concise, it should be somehow self-explanatory. When applicable please add `feat:` or `fix:` prefixes to the name.

_Example_

`feat: Initial project setup and configuration`, #1

### Reviewers
It is crucial to request a code review from one of team members. You cannot merge into main branch without the approve!

![Merge blocked](image-3.png)

### Projects
Don't forget to add appropriate values under the projects tab which represent the PR status, priority etc.

![PR Projects tab](image-2.png)

### Development
You can link PR to the Issue using Development tab. By doing that, Issue will be automatically closed (status: `Done`) when PR is successfully merged.

![Link issue](image-4.png)

### Merge
When you see a green light you can push the `Merge` button, leaving all the values as they are. After the successful merge you can delete the branch by clicking `Delete branch` button.

## Update you local repository
When changes were applied to the `main` branch in remote repository, you must also update your local repository with these changes.

To do that, you could __checkout__ (in your local repository) to the `main` branch and __pull__ the changes from remote repository.

After you can remove the branch you was working on in your local repository as you did it in the remote one.

---

Congratulations 🎉 Now you know how to contribute in this repository! Waiting for your awesome code ❤️ 
