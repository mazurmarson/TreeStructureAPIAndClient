import { HttpParams,HttpClient } from '@angular/common/http';
import { getInterpolationArgsLength } from '@angular/compiler/src/render3/view/util';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NodeService {

  baseUrl = 'https://localhost:5001/api/node'

constructor(private http: HttpClient) { }

getTree()
{
  return this.http.get(this.baseUrl);
}

deleteTree(id:number)
{
  return this.http.delete(this.baseUrl +'/' + id);
}

addNode(node:any)
{

  return this.http.post(this.baseUrl, node);
}

changeParent(nodeId:number, newParentId:number)
{
  return this.http.post(this.baseUrl +'/'+ nodeId+'/' + newParentId, null);
}



}

