# Thiago Valente

## Get started

Run `npm install` to install the UI project dependencies. Grab a cup of coffee or your beverage of choice.
You may also need to run `npm install start-server-and-test` and `npm install cross-env`.

## Development server

Run `npm run start` for a dev server. Navigate to http://localhost:4200/

## Navigating

1. Click on "Go to the Maze".
2. You can just select "Select a random maze" to get a random maze.
3. Or you can select an existing maze. (To import a new one, you can click on "Import a new maze" and you will be redirected to the import page).
4. After selecting a maze, you need to click on "Start".
5. A countdown will begin, and after that, the path in the maze will be hidden.
6. You can use the arrow keys on your keyboard to navigate through the maze.
    1. A timer will count the time you take to reach the end.
    2. A mistake counter will log every time you encounter a blocked path.
7. When you reach the end, a new record will be stored.
    1. And the list of the top 5 records (ordered by time) will be shown.
8. During the game, if you click on "Show Maze", the path will be displayed again.

1. When you are on the import page, you will be able to set a name and import a TXT file with the maze.
2. The system will validate if the name is filled and if the maze is valid. Below are the validation rules.
3. After a successful import, you will be able to select your maze on the previous page.

- There are some TXT files with maze examples on [root]\examples

## Frontend

1. There is a routing file (app-routing.module.ts) to define available routes.
2. All pages are in the Pages folder.
3. The maze structure is in the Maze folder.
    1. We have two services to connect to the backend (maze.service.ts and personal-record.service.ts).
    2. We have two subfolders: _models and import.
        1. _models: Contains all the models and enums specific to the maze context.
        2. import: Contains the structure for importing mazes.

## Backend

1. I created a simple N-Tier architecture (but I kept the simplicity and created everything in the same project).
2. I'm using DI for Services and Repositories.
3. I'm using basic REST API concepts.
3. There are 2 controllers (MazeController and PersonalRecordController).
4. Maze Structure:
    1. MazeController:
        1. POST Add (/mazes)
        2. GET GetAll (/mazes)
        3. GET GetById (/mazes/{id})
        4. GET GetRandom (/mazes/random)
        5. GET IsValidStep (/mazes/is-valid/{row}/{column})
    2. Maze Service:
    3. Maze Repository:
        1. The mazes that were imported will be stored in memory using a static list.
    4. Maze (domain):
        1. All rules to create a random maze and validate if it is valid are here.
            1. Most of these rules were created by searching the internet or with the help of ChatGPT
4. Personal Record Structure:
    1. PersonalRecordController:
        1. POST Add (/personal-records)
        4. DELETE ClearRecords (/personal-records) — not being used
        2. GET GetAll (/personal-records) — not being used
        3. GET GetTop5 (/personal-records/top5)
    2. PersonalRecord Service:
    3. PersonalRecord Repository:
        1. The personal records that were created will be stored in memory using a static list.
    4. PersonalRecord (domain):
5. Unit Tests:
    1. I created simple unit tests to validate the Maze domain class.

## Maze Validation Rules

1. The number of rows and columns must be between 3 and 10.
2. The number of rows and columns must be the same.
3. The 'S'tart of the maze must be the first position.
4. There must be one 'E'nd position.
5. The maze must have blocked positions ('X').
6. The maze must contain a valid path (starting at the 'S' position, passing through 'O' positions, and arriving at the 'E'nd position).






# Valant

This project was generated using [Nx](https://nx.dev).

[Nx Documentation](https://nx.dev/getting-started/nx-and-angular)

[Interactive Tutorial](https://nx.dev/angular-tutorial/01-create-application)

## Get started

Run `npm install` to install the UI project dependencies. Grab a cup of coffee or your beverage of choice.
You may also need to run `npm install start-server-and-test` and `npm install cross-env`

As you build new controller endpoints you can auto generate the api http client code for angular using `npm run generate-client:server-app`

## Development server

Run `npm run start` for a dev server. Navigate to http://localhost:4200/. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng g component my-component --project=demo` to generate a new component.

## Build

Run `ng build demo` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

- Run `ng test demo` to execute the unit tests via [Jest](https://jestjs.io).
- Run `nx affected:test` to execute the unit tests affected by a change.
- Run `npm run test:all` to run all unit tests in watch mode. They will re-run automatically as you make changes that affect the tests.
