import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth.component';
import { AuthGuard } from './shared/auth.guard';
import { AuthenticateUserResolverService } from './shared/authenticate-user-resolver.service';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: HomeComponent,
        canActivate: [AuthGuard],
        resolve: [AuthenticateUserResolverService]
    },
    {
        path: 'auth',
        component: AuthComponent
    }
];
