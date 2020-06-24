import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PublicComponent } from './public.component';
import { PublicHomeComponent } from './public-home/public-home.component';
import { RegisterNgoComponent } from './register-ngo/register-ngo.component';
import { SignInComponent } from './sign-in/sign-in.component';

const routes: Routes = [
    {
        path: '', component: PublicComponent,
        children: [
            { path: '', component: PublicHomeComponent },
            { path: 'ngo-tab', loadChildren: () => import('./ngo-tab/ngo-tab.module').then(m => m.NGOTabModule) },
            { path: 'register', component: RegisterNgoComponent },
            { path: 'sign-in', component: SignInComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PublicRoutingModule {
    static routedComponents = [PublicComponent, PublicHomeComponent, RegisterNgoComponent, SignInComponent];
}
