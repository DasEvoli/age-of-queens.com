![Age of Queens Banner](https://i.imgur.com/0TY1zSa.png)

# age-of-queens.com
This .NET Core Web Application is written for Age of Queens. Age of Queens is a community and a safe space for women who play Age of Empires to cultivate an environment that connects women to other women and encourage more to pick up the game.

## Requirements
* .NET Core 6.0 SDK
* Python 3.0 (For one script file)

## Build
* Building is easily possible in VSCode. I provided a launch.json and tasks.json file.
* If you want to build it yourself:
```
dotnet publish "ageofqueenscom.csproj" -c Release -p:EnvironmentName=Development
```
* Fill appsettings.json or use environment variables. For development I recommend user secrets.

## Run
```
dotnet ageofqueens.dll --urls=https://localhost:5001/
```

## Deploy
I use GitHub Actions to deploy this webapplication on my own webserver with every push on the main branch.

## Authors
![Image of DasEvoli](https://drive.google.com/uc?id=18704P2vcZ88vdlJ0WfDGNUZeG6qBTeNi) DasEvoli (Vinzenz Wetzel)

## License
* This project is licensed under the Apache License 2.0.
* All files that don't include any source code, especially image files, fall under copyright protection and you are not allowed to use them in your projects unless stated otherwise.