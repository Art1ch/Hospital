import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OfficesList } from './features/offices/components/offices-list/offices-list';
import { HomePage } from './features/home/components/home-page/home-page';

const routes: Routes = [
  {path: "", component: HomePage},
  {path: "offices", component: OfficesList},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
