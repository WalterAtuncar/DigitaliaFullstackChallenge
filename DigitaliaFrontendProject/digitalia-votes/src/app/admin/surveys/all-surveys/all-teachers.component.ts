import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TeachersService } from './teachers.service';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Teachers } from './teachers.model';
import { DataSource } from '@angular/cdk/collections';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, fromEvent, merge, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FormDialogComponent } from './dialogs/form-dialog/form-dialog.component';
import { DeleteDialogComponent } from './dialogs/delete/delete.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { SelectionModel } from '@angular/cdk/collections';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { User } from '../all-surveys/user.model';
import { Survey } from '../all-surveys/surveys';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
@Component({
  selector: 'app-all-teachers',
  templateUrl: './all-teachers.component.html',
  styleUrls: ['./all-teachers.component.sass'],
})
export class AllTeachersComponent
  extends UnsubscribeOnDestroyAdapter
  implements OnInit
{
  displayedColumns = [
    'surveyID',
    'title',
    'description',
    'question',
    'creationDate',
    'isActive',
    'actions',
  ];
  viewResult:boolean = true;
  exampleDatabase: TeachersService | null;
  dataSource = new MatTableDataSource<Survey>([]);
  selection = new SelectionModel<Survey>(true, []);
  isLoadingResults = false;
  accessId: number;
  //teachers: Teachers | null;
  userList: Survey[];
  user: Survey ={
    surveyID: 0,
    userID: 0,
    title: '',
    description: '',
    question: ''
  };
  breadscrums = [
    {
      title: 'Todas Encuestas',
      items: ['Encuestas'],
      active: 'Encuestas',
    },
  ];
  result: any[]=[];
  displayedColumnsResult: string[] = ['optionText', 'totalVotes'];
  constructor(
    public httpClient: HttpClient,
    public dialog: MatDialog,
    public teachersService: TeachersService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    super();
  }
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('filter', { static: true }) filter: ElementRef;
  @ViewChild(MatMenuTrigger)
  contextMenu: MatMenuTrigger;
  contextMenuPosition = { x: '0px', y: '0px' };

  ngOnInit() {
    this.loadData();
    this.dataSource.filterPredicate = (data: Survey, filter: string) => {
      const searchStr = (
        data.title +
        data.description +
        data.question +
        data.isActive
      ).toLowerCase();
      console.log("filtro", filter.toLowerCase())
      return searchStr.indexOf(filter.toLowerCase()) !== -1;
    };
  }
  applyFilter(filterValue: string) {
    console.log("filterValue", filterValue)
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  refresh() {
    this.loadData();
  }
  addNew() {
    let tempDirection;
    if (localStorage.getItem('isRtl') === 'true') {
      tempDirection = 'rtl';
    } else {
      tempDirection = 'ltr';
    }
    const dialogRef = this.dialog.open(FormDialogComponent, {
      data: {
        user: this.user,
        action: 'add',
      },
      direction: tempDirection,
    });
    this.subs.sink = dialogRef.afterClosed().subscribe((result) => {
      if (result === 1) {       
        this.loadData();
        this.showNotification(
          'snackbar-success',
          'Registro Agregado exitosamente...!!!',
          'bottom',
          'center'
        );
      }
    });
  }
  editCall(row) {
    this.accessId = row.accessId;
    let tempDirection;
    if (localStorage.getItem('isRtl') === 'true') {
      tempDirection = 'rtl';
    } else {
      tempDirection = 'ltr';
    }
    const dialogRef = this.dialog.open(FormDialogComponent, {
      data: {
        user: row,
        action: 'edit',
      },
      direction: tempDirection,
    });
    this.subs.sink = dialogRef.afterClosed().subscribe((result) => {
      if (result === 1) {       
        this.loadData();
        this.showNotification(
          'snackbar-success',
          'Registro Actualizado exitosamente...!!!',
          'bottom',
          'center'
        );
      }
    });
  }
  viewResults(row){
    this.viewResult = false;
    this.teachersService.getResults(row.surveyID).subscribe(res =>{
      console.log("result", res)
      this.result =res;
    })
  }
  deleteItem(row) {
    this.accessId = row.accessId;
    let tempDirection;
    if (localStorage.getItem('isRtl') === 'true') {
      tempDirection = 'rtl';
    } else {
      tempDirection = 'ltr';
    }
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: row,
      direction: tempDirection,
    });
    this.subs.sink = dialogRef.afterClosed().subscribe((result) => {
      if (result === 1) {       
        this.loadData();
        this.showNotification(
          'snackbar-danger',
          'Registro Inactivado exitosamente...!!!',
          'bottom',
          'center'
        );
      }      
    });
  }
  private refreshTable() {
    this.paginator._changePageSize(this.paginator.pageSize);
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    //const numRows = this.dataSource.renderedData.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
    // this.isAllSelected()
    //   ? this.selection.clear()
    //   : this.dataSource.renderedData.forEach((row) =>
    //       this.selection.select(row)
    //     );
  }
  removeSelectedRows() {
    const totalSelect = this.selection.selected.length;
    this.selection.selected.forEach((item) => {
      const index: number = this.dataSource.data.findIndex(d => d === item);
      this.dataSource.data.splice(index, 1);
      this.refreshTable();
      this.selection = new SelectionModel<Survey>(true, []);
    });
    // const totalSelect = this.selection.selected.length;
    // this.selection.selected.forEach((item) => {
    //   const index: number = this.dataSource.renderedData.findIndex(
    //     (d) => d === item
    //   );
    //   // console.log(this.dataSource.renderedData.findIndex((d) => d === item));
    //   this.exampleDatabase.dataChange.value.splice(index, 1);
    //   this.refreshTable();
    //   this.selection = new SelectionModel<User>(true, []);
    // });
    this.showNotification(
      'snackbar-danger',
      totalSelect + ' Record Delete Successfully...!!!',
      'bottom',
      'center'
    );
  }
  public loadData() {
    this.isLoadingResults = true;
    this.teachersService.getAllUsers().subscribe(data =>{
      console.log("data", data)
      this.dataSource = new MatTableDataSource<Survey>(data);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.isLoadingResults = false;
    }, error => {
      console.error(error);
      this.isLoadingResults = false;
    });
    // this.exampleDatabase = new TeachersService(this.httpClient);
    // this.dataSource = new ExampleDataSource(
    //   this.exampleDatabase,
    //   this.paginator,
    //   this.sort
    // );
    // this.subs.sink = fromEvent(this.filter.nativeElement, 'keyup').subscribe(
    //   () => {
    //     if (!this.dataSource) {
    //       return;
    //     }
    //     this.dataSource.filter = this.filter.nativeElement.value;
    //   }
    // );
  }
  goToVoting(row){
    this.teachersService.setdataVote(row);
    this.router.navigate(['/admin/systemUsers/edit-user']);
  }
  showNotification(colorName, text, placementFrom, placementAlign) {
    this.snackBar.open(text, '', {
      duration: 2000,
      verticalPosition: placementFrom,
      horizontalPosition: placementAlign,
      panelClass: colorName,
    });
  }
  // context menu
  onContextMenu(event: MouseEvent, item: User) {
    event.preventDefault();
    this.contextMenuPosition.x = event.clientX + 'px';
    this.contextMenuPosition.y = event.clientY + 'px';
    this.contextMenu.menuData = { item: item };
    this.contextMenu.menu.focusFirstItem('mouse');
    this.contextMenu.openMenu();
  }
}
export class ExampleDataSource extends DataSource<User> {
  filterChange = new BehaviorSubject('');
  get filter(): string {
    return this.filterChange.value;
  }
  set filter(filter: string) {
    this.filterChange.next(filter);
  }
  filteredData: User[] = [];
  renderedData: User[] = [];
  constructor(
    public exampleDatabase: TeachersService,
    public paginator: MatPaginator,
    public _sort: MatSort
  ) {
    super();
    // Reset to the first page when the user changes the filter.
    this.filterChange.subscribe(() => (this.paginator.pageIndex = 0));
  }
  /** Connect function called by the table to retrieve one stream containing the data to render. */
  connect(): Observable<User[]> {
    // Listen for any changes in the base data, sorting, filtering, or pagination
    const displayDataChanges = [
      this.exampleDatabase.dataChange,
      this._sort.sortChange,
      this.filterChange,
      this.paginator.page,
    ];
    this.exampleDatabase.getAllUsers();
    return merge(...displayDataChanges).pipe(
      map(() => {
        // Filter data
        this.filteredData = this.exampleDatabase.data
          .slice()
          .filter((users: User) => {
            const searchStr = (
              users.firstName +
              users.lastName +
              users.email +
              users.contactNumber +
              users.profileTypeId +
              users.isDeleted
            ).toLowerCase();
            return searchStr.indexOf(this.filter.toLowerCase()) !== -1;
          });
        // Sort filtered data
        const sortedData = this.sortData(this.filteredData.slice());
        // Grab the page's slice of the filtered sorted data.
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.renderedData = sortedData.splice(
          startIndex,
          this.paginator.pageSize
        );
        return this.renderedData;
      })
    );
  }
  disconnect() {}
  /** Returns a sorted copy of the database data. */
  sortData(data: User[]): User[] {
    if (!this._sort.active || this._sort.direction === '') {
      return data;
    }
    return data.sort((a, b) => {
      let propertyA: number | string = '';
      let propertyB: number | string = '';
      switch (this._sort.active) {
        case 'firstName':
          [propertyA, propertyB] = [a.firstName, b.firstName];
          break;
        case 'lastName':
          [propertyA, propertyB] = [a.lastName, b.lastName];
          break;
      }
      const valueA = isNaN(+propertyA) ? propertyA : +propertyA;
      const valueB = isNaN(+propertyB) ? propertyB : +propertyB;
      return (
        (valueA < valueB ? -1 : 1) * (this._sort.direction === 'asc' ? 1 : -1)
      );
    });
  }
}
