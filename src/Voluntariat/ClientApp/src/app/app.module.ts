import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AngularMaterialModule } from './shared/angular-material.module';
import { PublicModule } from './public/public.module';


const routes: Routes = [
    { path: '', loadChildren: () => import('./public/public.module').then(m => m.PublicModule) },

    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },

    { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
];


@NgModule({
    declarations: [
        AppComponent,
        CounterComponent,
        FetchDataComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        AngularMaterialModule,
        RouterModule.forRoot(routes),
        BrowserAnimationsModule,
        PublicModule
    ],
    exports: [
        AngularMaterialModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
