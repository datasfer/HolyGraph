import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HttpClientModule } from '@angular/common/http';
import { BookListComponent } from './books/book-list/book-list.component';
import { BookDetailComponent } from './books/book-detail/book-detail.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ArmatListComponent } from './armats/armat-list/armat-list.component';
import { HomeComponent } from './home/home.component';
import { ArmatTokensComponent } from './armats/armat-tokens/armat-tokens.component';
import { TokenListComponent } from './tokens/token-list/token-list.component';
import { ArmatAlphabeticalComponent } from './armats/armat-alphabetical/armat-alphabetical.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    BookListComponent,
    BookDetailComponent,
    ArmatListComponent,
    HomeComponent,
    ArmatTokensComponent,
    TokenListComponent,
    ArmatAlphabeticalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    HttpClientModule,
    PaginationModule.forRoot(),
    FormsModule,
    ButtonsModule.forRoot()      
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
