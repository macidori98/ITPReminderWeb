import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddNewCarComponent } from './add-new-car/add-new-car.component';
import { CarListComponent } from './car-list/car-list.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {path: 'carlist', component: CarListComponent, canActivate: [AuthGuard]},
      {path: 'addnewcar', component: AddNewCarComponent}
    ]
  },
  {path: '**', component: HomeComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
