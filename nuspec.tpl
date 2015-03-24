<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
	<metadata>
		<id>JF.AspNet.Identity.TableStorage</id>
		<version>1.0.0</version>
		<title>JF.AspNet.Identity.TableStorage</title>
		<authors>Josef Fazekas</authors>
		<owners>Josef Fazekas</owners>
		<licenseUrl>https://github.com/theuntitled/JF.AspNet.Identity.TableStorage/blob/master/LICENSE</licenseUrl>
		<projectUrl>https://github.com/theuntitled/JF.AspNet.Identity.TableStorage</projectUrl>
		<iconUrl>https://raw.githubusercontent.com/theuntitled/JF.AspNet.Identity.TableStorage/master/JF.AspNet.Identity.TableStorage/theuntitled.ico</iconUrl>
		<requireLicenseAcceptance>false</requireLicenseAcceptance>
		<description>AspNet Identity Azure Table Storage Implementation.</description>
		<copyright>Copyright © Josef Fazekas 2015</copyright>
		<language>en-GB</language>
		<tags>AspNet, Identity, Azure, Table Storage</tags>
		<dependencies>
			<group targetFramework=".NETFramework4.5.1">
				<dependency id="JF.Azure.TableStorage" version="1.0.0" />
				<dependency id="JF.Build.Tasks" version="1.0.0" />
				<dependency id="Microsoft.AspNet.Identity.Core" version="2.2.0" />
				<dependency id="Microsoft.Data.Edm" version="5.6.2" />
				<dependency id="Microsoft.Data.OData" version="5.6.2" />
				<dependency id="Microsoft.Data.Services.Client" version="5.6.2" />
				<dependency id="Microsoft.WindowsAzure.ConfigurationManager" version="2.0.3" />
				<dependency id="Newtonsoft.Json" version="5.0.8" />
				<dependency id="System.Spatial" version="5.6.2" />
				<dependency id="WindowsAzure.Storage" version="4.3.0" />
			</group>
		</dependencies>
		<frameworkAssemblies>
			<frameworkAssembly assemblyName="System" targetFramework=".NETFramework4.5.1" />
			<frameworkAssembly assemblyName="System.Core" targetFramework=".NETFramework4.5.1" />
			<frameworkAssembly assemblyName="System.Data" targetFramework=".NETFramework4.5.1" />
		</frameworkAssemblies>
	</metadata>
	<files>
		<file src="JF.AspNet.Identity.TableStorage\bin\Release\JF.AspNet.Identity.TableStorage.XML" target="lib\JF.AspNet.Identity.TableStorage.XML" />
		<file src="JF.AspNet.Identity.TableStorage\bin\Release\JF.AspNet.Identity.TableStorage.pdb" target="lib\JF.AspNet.Identity.TableStorage.pdb" />
		<file src="JF.AspNet.Identity.TableStorage\bin\Release\JF.AspNet.Identity.TableStorage.dll" target="lib\JF.AspNet.Identity.TableStorage.dll" />
	</files>
</package>