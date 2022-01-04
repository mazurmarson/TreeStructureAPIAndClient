import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { NodeService } from '../_services/Node.service';




@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit {

  tree: any;
  selectedItem: any;
  currentId = null;
  newParrentId: any;
  newNode: any = {};
  sortBy = 0;

  constructor(private nodeService: NodeService, private authService: AuthService) { }

  ngOnInit() {
    this.loadTree();
    this.sortBy = 0;
  }



listClick(event, newValue) {
    console.log(newValue);
    this.currentId = newValue.id;
    this.selectedItem = newValue;
    newValue.showTree = !newValue.showTree
    event.stopPropagation()
  }

  loadTree()
  {
    this.nodeService.getTree().subscribe( (response : any ) => {
      this.tree = response;
      console.log(this.tree);
    }, error => {
      console.log('Nie udało się pobrać drzewa');
    } );
  }

  deleteNode()
  {
    this.nodeService.deleteNode(this.currentId).subscribe( (response: any) => {
      console.log(response);
      this.loadTree();
    }, error => {
      console.log('Nie udało się usunąć węzłu');
    } );

    this.ngOnInit();
  }

  changeParent()
  {
    this.nodeService.changeParent(this.currentId, this.newParrentId).subscribe( (respone : any) => {
      console.log(respone);
      this.loadTree();
    }, error => {
      console.log('Nie udało się edytować rodzica');
    });


  }

  addNewNode()
  {
   this.newNode.parentId = this.currentId;
    this.nodeService.addNode(this.newNode).subscribe( (response:any) => {
      console.log(response);
      this.loadTree();
    }, error => {
      alert(error);
    });


  }

  sort(sortBy:number)
  {
    this.sortBy = sortBy;
    this.nodeService.getTreeSorted(sortBy).subscribe( (response : any ) => {
      this.tree = response;
      console.log(this.tree);
    }, error => {
      console.log('Nie udało się pobrać drzewa');
    } );
  }

  resetCurrentNodeValue()
  {
    this.currentId = null;
    console.log(this.currentId);
  }

  checkRole()
  {
    return this.authService.checkRole();
  }
}
