using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace WebJobsDemo.SampleJobs
{
    public class WebImageBinder : ICloudBlobStreamBinder<WebImage>
    {
        public Task<WebImage> ReadFromStreamAsync(Stream input,
            System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult<WebImage>(new WebImage(input));
        }
        public Task WriteToStreamAsync(WebImage value, Stream output,
            System.Threading.CancellationToken cancellationToken)
        {
            var bytes = value.GetBytes();
            return output.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        }
    }
}
