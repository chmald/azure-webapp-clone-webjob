# Azure Web App Clone via Web Job
This Azure Web Job will help you clone your site's wwwroot directory to another Web App via Zip Deploy.

## To configure
In `App.config`, add your $user:password and web app name in settings.

Ex.

```
    <add key="userPass" value="$username:password-hash" />
    <add key="webApp" value="site-name" />
```

You can get your user and password by downloading your Publishing Profile for the destination Web App.
[How to download Publish Profile](https://docs.microsoft.com/en-us/visualstudio/deployment/tutorial-import-publish-settings-azure?view=vs-2017#create-the-publish-settings-file-in-azure-app-service)

Deploy project as Azure Web Job to your primary site.

You can use Continuous or Triggered web job.

*Happy Cloning!*
