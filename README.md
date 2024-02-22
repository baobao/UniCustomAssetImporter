# UniCustomAssetImporter
A Unity editor extension to customize asset settings during AssetImport.

## How to install

Please enter the following GitHub URL from PackageManager.
`https://github.com/baobao/UniCustomAssetImporter.git?path=Assets/UniCustomAssetImporter`

![](https://i.gyazo.com/ba6beaa24ffd3e7509c056749fbbcc5e.jpg)  

## How to Use

The `Assets/Tools/UniCustomAssetImporter` folder is automatically created.
Select UniCustomAssetImporter and look at the Inspector window.


![](https://i.gyazo.com/e26c1554e3ae222e5ff52e365307dfde.jpg)  
Press the Create Setting button to create a setting file. Multiple setting files can be created.

![](https://i.gyazo.com/a245f8fc084117fe31891331410b507e.png)  
Once the configuration file is created, rename it. In this case, we named it "UI".

![](https://i.gyazo.com/efe5013f836ab5bcf8252f1e687f047b.png)  
Create a configuration file like this.

Then, let's reimport the image file. The settings should then be reflected.From now on, the settings will be applied each time an image file is added.

## When trouble

![](https://i.gyazo.com/d8c8ccfa23efb122ddaed91066dea323.jpg)  
If it does not work, check to see if IsEnabled is checked.


## License

This library is under the MIT License.
