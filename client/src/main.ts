import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment.development';


platformBrowserDynamic().bootstrapModule(AppModule).then(ref => {
  if (environment.production) {
    window.console.log = () => { }
}
}).catch(err => console.error(err));
