import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './books/book-list/book-list.component';
import { ArmatListComponent } from './armats/armat-list/armat-list.component';
import { BookDetailComponent } from './books/book-detail/book-detail.component';
import { HomeComponent } from './home/home.component';
import { ArmatTokensComponent } from './armats/armat-tokens/armat-tokens.component';
import { TokenListComponent } from './tokens/token-list/token-list.component';
import { ArmatAlphabeticalComponent } from './armats/armat-alphabetical/armat-alphabetical.component';

const routes: Routes = [
 {path: '', component: HomeComponent},
 {path: 'books', component: BookListComponent},
 {path: 'armats', component: ArmatListComponent},
 {path: 'armats/:startsWith', component: ArmatListComponent},
 //{path: 'armats/:startsWith', component: ArmatAlphabeticalComponent},
 {path: 'armat-tokens/:armat', component: ArmatTokensComponent},
 {path: 'tokens', component: TokenListComponent}
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
