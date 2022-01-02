import { Component, OnInit } from '@angular/core';
import { NodeService } from '../_services/Node.service';




@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit {

  tree: any;
  selectedItem: any;
  currentId = 0;
  newParrentId: any;
  newNode: any = {};
  sortBy = 0;

  constructor(private nodeService: NodeService) { }

  ngOnInit() {
    this.loadTree();

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

  deleteTree()
  {
    this.nodeService.deleteTree(this.currentId).subscribe( (response: any) => {
      console.log(response);
    }, error => {
      console.log('Nie udało się usunąć węzłu');
    } );

    this.loadTree();
  }

  changeParent()
  {
    this.nodeService.changeParent(this.currentId, this.newParrentId).subscribe( (respone : any) => {
      console.log(respone);
    }, error => {
      console.log('Nie udało się edytować rodzica');
    });

    this.loadTree();
  }

  addNewNode()
  {
   this.newNode.parentId = this.currentId;
    this.nodeService.addNode(this.newNode).subscribe( (response:any) => {
      console.log(response);
    }, error => {
      console.log('Nie udało sie dodać nowego węzła')
    });
    this.loadTree();

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

}
