import { Component, Inject, NgZone, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html'
})

export class ListComponent {
  public animals: Animal[]; // List of animals to be displayed
  private baseUrl: string; // Location of back end to query

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private changeDetector: ChangeDetectorRef) {
    http.get<Animal[]>(baseUrl + 'api/List/get').subscribe(result => {
      this.animals = result;
      this.baseUrl = baseUrl;
    }, error => console.error(error));
  }

  // User adds an animal
  addAnimal(name: string): void {
    // Send back end a get request to add an item defined by name
    this.http.get(this.baseUrl + 'api/List/add/' + name).subscribe();
    // Update the list of animals on the front end
    this.updateAnimals();
    this.changeDetector.detectChanges();
  }

  // User removes an animal
  removeAnimal(id: number): void {
    // Send back end a delete request to remove an item at an index id
    this.http.delete(this.baseUrl + 'api/List/delete/' + id).subscribe();
    // Update the list of animals on the front end
    this.updateAnimals();
    this.changeDetector.detectChanges();
  }

  // Page updates to show all current animals
  updateAnimals(): void {
    // Get list of animals from the back end, and update front end
    this.http.get<Animal[]>(this.baseUrl + 'api/List/get').subscribe(result => {
      this.animals = result;
    }, error => console.error(error));
  }
}

interface Animal {
  id: number; // Position on list
  name: string; // Name of animal
}
