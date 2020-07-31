import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WelcomeComponent } from './welcome.component';
import { NzGridModule } from 'ng-zorro-antd';

const routes: Routes = [
  { path: '', component: WelcomeComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes), NzGridModule],
  exports: [RouterModule, NzGridModule]
})
export class WelcomeRoutingModule { }
