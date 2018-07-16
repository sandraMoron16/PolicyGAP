import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { PolicyComponent } from './components/policy/policy.component';

import { PoliciesService } from "./providers/policies.service";
import { HomeComponent } from './components/home/home.component';

import { HttpClientModule } from "@angular/common/http";
import { FormsModule,ReactiveFormsModule } from "@angular/forms";
import { app_routing } from "./app.route";
import { HttpModule } from "@angular/http";
@NgModule({
  declarations: [
    AppComponent,
    PolicyComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    app_routing,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule
  ],
  providers: [
    PoliciesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
