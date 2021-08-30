# Utility

Utility package containing Animator, Vector, List etc. extensions to help developers

## Features

- Utils static class contains many handy functions and class extentions

## Installation

There are thre ways to use it

### Unitypackage
Go to the releases section and download the .unitypackage file

### PackageManager
##### Scoped Registry
TO add Joyixir scope to NPM scopedregistry, add the following to manifest.json
```json
{
    "scopedRegistries": [
      {
        "name": "npmjs",
        "url": "https://registry.npmjs.org/",
        "scopes": [
          "com.joyixir"
        ]
      }
    ]
}
```

##### Install the package
Open Window/PackageManager and head to My Registries. Install your desired version of Joyixir/Utility

### Clone the repo
You can just clone the repo and do whatever you like with it. Even to make it better.


# Use
### Utils
Just look around to see if there's any useful extensions for you. There are extensions for AnimationCurve, Vector3, List, Animator, IEnumerable<Color>, Collider Bounds, KeyFrame

There are also some handy functions that you can use(Normalizers, etc).
### TimeScaler
Attach TimeScaler to a GameObject
Do
```csharp
TimeScaler.ScaleUnityTime(float: slowDownFactor);
```
To undo any scale factor, do:
```csharp
TimeScaler.SetUnityTimeScalesToNormal();
```
## License

MIT