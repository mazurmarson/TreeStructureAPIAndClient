import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { AppComponent } from './app.component';
import { TreeComponent } from './tree/tree.component';
import { NodeService } from './_services/Node.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { JwtModule } from '@auth0/angular-jwt';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { ErrorInterceptorProvider } from './_services/error.interceptor';



export function tokenGetter()
{
  return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    TreeComponent,
      NavComponent,
      RegisterComponent,
      HomeComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    JwtModule.forRoot(
      {
        config: {
          tokenGetter,
          allowedDomains: ['localhost:5001'],
          disallowedRoutes: ['localhost:5001/api/auth']
        }
      }
    ),

    RouterModule.forRoot(appRoutes),

  ],
  providers: [
    NodeService,
    AuthService,
    AuthGuard,
    ErrorInterceptorProvider




  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
