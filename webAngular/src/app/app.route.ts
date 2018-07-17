import { RouterModule,Routes } from "@angular/router";
import { PolicyComponent } from "./components/policy/policy.component";

const APP_ROUTES: Routes=[

    { path:'policies',component:PolicyComponent},
    { path:'**',pathMatch:'full', redirectTo:'policies'}
];

export const app_routing= RouterModule.forRoot(APP_ROUTES)