import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ArmatParams } from '../_models/armatParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { environment } from 'src/environments/environment.development';
import { Armat } from '../_models/armat';
import { map } from 'rxjs';
import { Token } from '../_models/token';

@Injectable({
  providedIn: 'root'
})
export class ArmatsService {

  constructor(private http: HttpClient) {

   }

  getArmats(armatParams: ArmatParams) {
  let params = getPaginationHeaders(armatParams.scripture, armatParams.pageNumber, armatParams.pageSize);
  return getPaginatedResult<Armat[]>(environment.apiUrl + 'armats', params, this.http).pipe(
    map((response: any) => {
      console.log(response);
      return response;
    })
  )
}


getArmatsStartsByLetter(startsWith: string) {
  let params = new HttpParams();
  params = params.append('ignoreCase', false);
  return this.http.get<Token[]>(environment.apiUrl +`armats/${startsWith}`, { observe: 'response', params }).pipe(
    map((response) => {
      console.log(response.body);
      return response.body;
    })
  );
}


getTokensByArmatID(id: number) {
  return this.http.get<Token[]>("", { observe: 'response' }).pipe(
    map(response => {
      console.log(response);
      return response;
    })
  );
}

getTokensByArmat(armat: string) {
  let params = new HttpParams();
  params = params.append('armat', armat);
  return this.http.get<Token[]>(environment.apiUrl + 'tokens', { observe: 'response', params }).pipe(
    map((response) => {
      console.log(response.body);
      return response.body;
    })
  );
}


}
