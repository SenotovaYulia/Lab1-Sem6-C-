#include "pch.h"
#include "mkl.h"
#include <mkl_df_defines.h>
#include <iostream>
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}



extern "C"  _declspec(dllexport) void GlobalFunction(int& ret, int note_number, double* notes, double* measures, double* derivatives,
    int new_note_number, double* new_grid, double* new_values, double* left_integ, double* right_integ, double* integrals,
    double* spline_coeff)
{
    try {
        DFTaskPtr task;
        int NY = 1;
        auto status = dfdNewTask1D(&task, note_number, notes, DF_NON_UNIFORM_PARTITION, NY, measures, DF_MATRIX_STORAGE_ROWS);
        status = dfdEditPPSpline1D(task, DF_PP_CUBIC, DF_PP_NATURAL, DF_BC_1ST_LEFT_DER | DF_BC_1ST_RIGHT_DER, derivatives,
            DF_NO_IC, NULL, spline_coeff, DF_NO_HINT);
        status = dfdConstruct1D(task, DF_PP_SPLINE, DF_METHOD_STD);
        int dorder[3]{ 1, 1, 1 };
        status = dfdInterpolate1D(task, DF_INTERP, DF_METHOD_PP, new_note_number,
            new_grid, DF_NON_UNIFORM_PARTITION, 3, dorder, NULL, new_values, DF_MATRIX_STORAGE_ROWS, NULL);
        status = dfdIntegrate1D(task, DF_METHOD_PP, 1, left_integ, DF_NON_UNIFORM_PARTITION,
            right_integ, DF_NON_UNIFORM_PARTITION, NULL, NULL, integrals, DF_MATRIX_STORAGE_ROWS);
        status = dfDeleteTask(&task);
        ret = 0;
    }
    catch (...) {
        //std:: cout << "here";
        ret = -1;
    }
}