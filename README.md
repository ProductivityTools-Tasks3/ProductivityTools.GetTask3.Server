# ProductivityTools.GetTask3.Server

 ```
 application with phsyical root failed to load corecrl exception message: Managed server didnt initalize after 120000ms restart
```

```
Otwórz IIS Manager na serwerze.
Przejdź do Application Pools i wybierz pulę aplikacji, z której korzysta serwis.
Kliknij w Advanced Settings (Ustawienia zaawansowane):
Zmień Idle Time-out (minutes) z domyślnych 20 na 0.
Zmień Start Mode (w sekcji Process Model) na AlwaysRunning.
Przejdź na wykaz witryn, kliknij na swoją aplikację prawym przyciskiem myszy -> Manage Website -> Advanced Settings:
Opcję Preload Enabled ustaw na True.
```
