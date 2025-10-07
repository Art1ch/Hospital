import { Routes } from "@angular/router";
import { HomePage } from "./features/home/components/home-page/home-page";
import { OfficesList } from "./features/offices/components/offices-list/offices-list";

export const routes: Routes = [
  { path: '', component: HomePage },
  { path: 'offices', component: OfficesList },
];