# monogame

## Template
```
$ dotnet new -i MonoGame.Templates.CSharp::3.8.0.1641
```

## Project
```
$ dotnet new mgdesktopgl -o YourGameFolderNa
```

## Content Builder
Inside the project
```
$ dotnet tool install --global dotnet-mgcb --version 3.8.0.1641
```

## Content Editor
```
$ dotnet tool install --global dotnet-mgcb-editor --version 3.8.0.1641
```

### zsh
```
$ cat << \EOF >> ~/.zprofile
# Add .NET Core SDK tools
export PATH="$PATH:/Users/<your_user_name>/.dotnet/tools"
EOF

$ zsh -l
```

# Register
```
$ mgcb-editor --register

$ mgcb-editor
```