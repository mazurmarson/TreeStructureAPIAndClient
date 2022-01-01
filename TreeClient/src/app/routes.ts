import { Routes } from '@angular/router'
import { HomeComponent } from './home/home.component'
import { RegisterComponent } from './register/register.component'
import { TreeComponent } from './tree/tree.component'

export const appRoutes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'tree', component: TreeComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', redirectTo: 'home', pathMatch: 'full'},
];
