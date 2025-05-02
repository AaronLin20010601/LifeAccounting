export async function downloadExcelFile(fetchApi, filterOptions) {
    try {
        const data = await fetchApi(filterOptions);
        const blob = new Blob([data], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        });

        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');

        const timestamp = new Date().toISOString().slice(0, 19).replace(/[-T:]/g, '');
        link.href = url;
        link.setAttribute('download', `records_${timestamp}.xlsx`);
        document.body.appendChild(link);
        link.click();
        link.remove();

        window.URL.revokeObjectURL(url);
    } catch (error) {
        console.error(error);
        throw new Error('Excel download failed.');
    }
}