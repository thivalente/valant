import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { LoggingService } from '../../../logging/logging.service';
import { MazeService } from '../maze.service';
import { MazeStatusEnum } from '../_models/maze-status.enum';
import { ApiResponse } from '../../../api-client/api-response.model';

@Component({ selector: 'app-maze-import', templateUrl: './maze-import.component.html', styleUrls: ['./maze-import.component.less'] })
export class MazeImportComponent implements OnInit, AfterViewInit {
    @ViewChild('fileInput') fileInput: ElementRef;
    @ViewChild('nameInput') nameInput: ElementRef;
    
    public form: FormGroup;
    public file: File | null = null;
    public fileName: string = '';
    private fileContent: string[][] = [];

    constructor(
        private logger: LoggingService,
        private mazeService: MazeService,
        private fb: FormBuilder,
        private router: Router) { }

    ngOnInit(): void {
        this.form = this.fb.group({
            name: ['', [Validators.required, Validators.maxLength(20)]],
            file: [null, [Validators.required, this.fileValidator]]
        });
    }

    ngAfterViewInit(): void {
        this.nameInput.nativeElement.focus();
    }

    private fileValidator(control: AbstractControl): ValidationErrors | null {
        const file = control.value;

        if (file) {
          const fileName: string = file.name;
          const fileExtension = fileName.split('.').pop().toLowerCase();

          if (fileExtension !== 'txt')
            return { invalidFileType: true };
        }

        return null;
    }

    onFileChange(event: Event): void {
        const input = event.target as HTMLInputElement;

        if (input.files && input.files.length) {
            this.file = input.files[0];

            if (this.file.type !== 'text/plain') {
                this.form.get('file').markAsDirty();
                this.form.get('file').setErrors({ invalidFileType: true });
            }
            else {
                this.form.patchValue({ file: this.file });
                this.form.get('file').updateValueAndValidity();

                this.readFile(this.file);

                this.fileName = this.file.name;
            }
        }

        this.fileInput.nativeElement.value = '';
    }

    onSubmit(): void {
        if (this.form.valid && this.file) {
            const mazeName = this.form.get('name').value;

            this.mazeService.add(mazeName, this.fileContent).subscribe({
                next: (res: ApiResponse<any>) => {
                    if (!res || !res.success) {
                        console.log('Errors:', res.result);
                        alert('Error importing the maze. See the console for details');
                        return;
                    }

                    alert('Import successful');
                    this.router.navigate(['/maze']);
                },
                error: (error) => {
                  this.logger.error('Error adding maze: ', error);
                }
            })

            
        }
    }

    private processFileContent(text: string): string[][] {
        const validLetters = [MazeStatusEnum.Start.toString().toUpperCase(), MazeStatusEnum.Go.toString().toUpperCase(), MazeStatusEnum.NotGo.toString().toUpperCase(), MazeStatusEnum.End.toString().toUpperCase()];
        const result = text.split('\n').map(line => line.split('').filter(char => validLetters.includes(char))).filter(row => row.length > 0);

        return result;
    }

    private readFile(file: File): void {
        const reader = new FileReader();

        reader.onload = () => {
          const text = reader.result as string;
          this.fileContent = this.processFileContent(text);
        };

        reader.readAsText(file);
    }

    triggerFileInput(): void {
        this.fileInput.nativeElement.click();
    }
}