export interface MazeCurrent {
    column: number;
    row: number;
}

export interface MazeStep {
    step: string;
    walked: boolean;
}