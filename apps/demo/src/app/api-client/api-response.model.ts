export interface ApiResponse<Type> {
    success: boolean;
    statusCode: number;
    requestId: string;
    message: string;
    result: Type;
}