import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './books/book-list/book-list.component';

const routes: Routes = [
 // {path: '', component: HomeComponent},
 {path: 'books', component: BookListComponent}
  // {
  //   path: '',
  //   runGuardsAndResolvers: 'always',
  //  // canActivate: [authGuard],
  //   children: [
  //     {path: 'books', component: BookListComponent},
  //     // {path: 'members/:username', component: MemberDetailComponent, resolve: {member: memberDetailedResolver}},
  //     // {path: 'member/edit', component: MemberEditComponent, canDeactivate: [preventUnsavedChangesGuard]},
  //     // {path: 'lists', component: ListsComponent},
  //     // {path: 'messages', component: MessagesComponent},
  //     // {path: 'admin', component: AdminPanelComponent, canActivate: [adminGuard]},
  //   ]
  // },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
