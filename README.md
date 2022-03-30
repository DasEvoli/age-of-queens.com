![Age of Queens Banner](https://i.imgur.com/0TY1zSa.png)

# age-of-queens.com
This .NET Core MVC Web Application is written for Age of Queens. Age of Queens is a community and a safe space for women who play Age of Empires to cultivate an environment that connects women to other women and encourage more to pick up the game.

## Contributing
If you are a member of the Age of Queens community or you are approved as an editor you can contribute by doing pull requests.

## Requirements
* .NET Core 6.0 SDK
* Python 3.0 (For one script file)

## Build
* Building is easily possible in VSCode. I provided a launch.json and tasks.json file.
* If you want to build it yourself:
```
dotnet debug "ageofqueenscom.csproj" -p:EnvironmentName=Development
```
* Fill appsettings.json or use environment variables. For development I recommend user secrets.
* Some content will be loaded by .csv files. They are automatically getting updated via Python script.

## Run
```
dotnet ageofqueens.dll --urls=https://localhost:5001/
```

## Deploy
I use GitHub Actions to deploy this webapplication on my own webserver with every push on the main branch.

## Todo
- [ ] Create ogImage for every site
- [ ] Refactor site.css and the views accordingly
- [X] Add Error Middleware
- [ ] Improve mobile version
- [ ] Small Thumbnails for Mod Page

## Authors
![Image of DasEvoli](https://i.imgur.com/xNcLWUT.png) DasEvoli (Vinzenz Wetzel)

## License
* This project is licensed under the Apache License 2.0.
* All files that don't include any source code, especially image files, fall under copyright protection and you are not allowed to use them in your projects unless stated otherwise.
